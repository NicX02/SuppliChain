using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SuppliChain.Models;
using Microsoft.AspNetCore.Http;
using SuppliChain.Data;

namespace SuppliChain.Controllers;

public class HomeController : BaseController
{
    private readonly ILogger<HomeController> _logger;
    private readonly DatabaseContext _context;

    public HomeController(DatabaseContext context, ILogger<HomeController> logger)
    {
        _context = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    // public IActionResult Login()
    // {
    //     return View();
    // }
    public IActionResult Out()
    {
        return View();
    }
    public IActionResult In()
    {
        return View();
    }

    public IActionResult NewItem()
    {
        return View();
    }

    public IActionResult Admin()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId.HasValue && _context.Users.Single(u => u.UserId == userId.Value).Role == "admin")
        {
            // is admin
            return View();
        }
        return Redirect("PremissionDenied");
    }
    public IActionResult PremissionDenied()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    [HttpPost]
    public JsonResult GetInDB()
    {
        var items = _context.Stocks
            .Join(_context.Items,
                stock => stock.ItemId,
                item => item.ItemId,
                (stock, item) => new
                {
                    ItemId = item.ItemId,
                    ItemName = item.Name,
                    Count = stock.Quantity
                })
            .ToArray();
        return Json(items);
    }

    [HttpPost]
    public JsonResult GetCategories()
    {
        var categories = _context.ItemCategories
            .Select(ic => ic.Name)
            .ToArray();
        return Json(categories);
    }



    public IActionResult AddItemToDB(int itmID, int Count)
    {

        var item = _context.Items
            .FirstOrDefault(itm => itm.ItemId == itmID);
        if (item == null)
        {
            ViewBag.Message = $"Item with ID {itmID} does not exist in the database.";
            return View("In");
        }
        if (Count <= 0)
        {
            ViewBag.Message = $"Number to Add must be greater than 0, but was {Count}.";
            return View("In");
        }
        else
        {
            var stock = _context.Stocks
                .FirstOrDefault(s => s.ItemId == itmID);

            if (stock != null)
            {
                stock.Quantity += Count;
            }
            else
            {
                ViewBag.Message = $"Stock for item with ID {itmID} does not exist.";
                return View("In");
            }
        }
        _context.SaveChanges();
        return RedirectToAction("In");
    }


    public IActionResult RemoveItemFromDB(int itmID, int Count)
    {

        var item = _context.Items
            .FirstOrDefault(itm => itm.ItemId == itmID);
        if (item == null)
        {
            ViewBag.Message = $"Item with ID {itmID} does not exist in the database.";
            return View("Out");
        }
        if (Count <= 0)
        {
            ViewBag.Message = $"Number to Add must be greater than 0, but was {Count}.";
            return View("Out");
        }
        else
        {
            var stock = _context.Stocks
                .FirstOrDefault(s => s.ItemId == itmID);
            if (stock != null)
            {
                if (stock.Quantity < Count)
                {
                    ViewBag.Message = $"Not enough of the item in stock. Requested {Count}, but only {stock.Quantity} in stock.";
                    return View("Out");
                }
                stock.Quantity -= Count;
            }
            else
            {
                ViewBag.Message = $"Stock for item with ID {itmID} does not exist.";
                return View("Out");
            }
        }
        _context.SaveChanges();
        return RedirectToAction("Out");
    }

    public IActionResult AddNewItemToDB(string name, string description, decimal price, string categoryName)
    {
        var category = _context.ItemCategories
            .FirstOrDefault(c => c.Name == categoryName);

        if (category == null)
        {
            ViewBag.Message = $"Category '{categoryName}' does not exist.";
            return View("NewItem");
        }
        if (name == null || name == "" || description == null || description == "" || price < 1)
        {
            ViewBag.Message = $"Input data is invalid.";
            return View("NewItem");
        }

        var categoryID = category.CategoryId;
        _context.Items.Add(new Item { Name = name, Description = description, Price = price, ItemCategoryId = categoryID, Stock = new Stock { Quantity = 0 } });
        _context.SaveChanges();
        return View("NewItem");
    }


}

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
}

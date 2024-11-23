using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SuppliChain.Models;
using Microsoft.AspNetCore.Http;

namespace SuppliChain.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
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
        if (HttpContext.Session.GetInt32("UserId") == 123) //TODO database lookup
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

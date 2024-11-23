using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SuppliChain.Models;

namespace SuppliChain.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;

    public LoginController(ILogger<LoginController> logger)
    {
        _logger = logger;
    }

    // public IActionResult Index()
    // {
    //     return View();
    // }

    // public IActionResult Privacy()
    // {
    //     return View();
    // }
    public IActionResult Login()
    {
        ViewBag.HideLinks = true;
        return View();
    }

    [HttpPost]
    public ActionResult Login(string username, string password, string action)
    {
        if (action == "Login")
        {
            if (username == "admin" && password == "admin")
            {
                HttpContext.Session.SetInt32("UserId", 456);//TODO user id
                HttpContext.Session.SetString("UserName", username);
                return RedirectToAction("Index", "Home");
            }
        }
        else if (action == "Register")
        {
            if (username == "admin" && password == "admin")
                return RedirectToAction("Privacy", "Home");
        }
        LoginModel lrm = new()
        {
            ErrorName = "ERROR"
        };
        ViewBag.HideLinks = true;
        return View(lrm);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

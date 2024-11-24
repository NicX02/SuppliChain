using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuppliChain.Data;
using SuppliChain.Models;

namespace SuppliChain.Controllers;

public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly DatabaseContext _context;

    public LoginController(DatabaseContext context, ILogger<LoginController> logger)
    {
        _context = context;
        _logger = logger;
    }
    public IActionResult Login()
    {
        ViewBag.HideLinks = true;
        return View();
    }

    [HttpPost]
    public async Task<ActionResult> Login(string username, string password, string action)
    {
        if (action == "Login")
        {
            bool isAuthenticated = await AuthenticateUser(username, password);
            if (isAuthenticated)
            {
                HttpContext.Session.SetInt32("UserId", 456);//TODO user id
                HttpContext.Session.SetString("UserName", username);
                return RedirectToAction("Index", "Home");
                // return Ok(new { Message = "Login successful" });
            }
        }
        else if (action == "Register")
        {
            // if (username == "admin" && password == "admin")
            return RedirectToAction("Privacy", "Home");
        }

        LoginModel lrm = new()
        {
            ErrorName = "ERROR"
        };
        ViewBag.HideLinks = true;
        return View(lrm);
        // return Unauthorized(new { Message = "Invalid username or password" });

        //     if (action == "Login")
        //     {


        //         if (username == "admin" && password == "admin")
        //         {
        //             HttpContext.Session.SetInt32("UserId", 456);//TODO user id
        //             HttpContext.Session.SetString("UserName", username);
        //             return RedirectToAction("Index", "Home");
        //         }
        //     }
        //     else if (action == "Register")
        //     {
        //         if (username == "admin" && password == "admin")
        //             return RedirectToAction("Privacy", "Home");
        //     }
        //     LoginModel lrm = new()
        //     {
        //         ErrorName = "ERROR"
        //     };
        //     ViewBag.HideLinks = true;
        //     return View(lrm);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<bool> AuthenticateUser(string username, string password)
    {
        // Search for the user by email
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Name == username);

        if (user != null)
        {
            // Verify the password hash
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                // Check if the user is an admin
                if (user.Role == "admin")
                {
                    Console.WriteLine("User is an admin.");
                }
                else
                {
                    Console.WriteLine("User is not an admin.");
                }
                return true; // Login success
            }
        }

        Console.WriteLine("Invalid username or password.");
        return false; // Login failed
    }

}

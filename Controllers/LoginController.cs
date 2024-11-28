using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuppliChain.Data;
using SuppliChain.Models;

namespace SuppliChain.Controllers;

public class LoginController : BaseController
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

    public IActionResult Register()
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
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == username);
                if (user == null)
                    return RedirectToAction("Login", "Login");
                HttpContext.Session.SetInt32("UserId", user.UserId);
                HttpContext.Session.SetString("UserName", username);
                if (user.Role == "admin")
                    HttpContext.Session.SetString("IsAdmin", "true");
                else
                    HttpContext.Session.SetString("IsAdmin", "false");
                return RedirectToAction("Index", "Home");
                // return Ok(new { Message = "Login successful" });
            }
        }
        else if (action == "Register")
        {
            // if (username == "admin" && password == "admin")
            return RedirectToAction("Register", "Login");
        }

        LoginModel lrm = new()
        {
            ErrorName = "Invalid username or password"
        };
        ViewBag.HideLinks = true;
        return View(lrm);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.SetInt32("UserId", -1);
        HttpContext.Session.SetString("UserName", "");
        return Redirect("/");
    }


    public async Task<ActionResult> RegisterCall(string email, string username, string password, string action)
    {
        if (email == null || username == null || password == null || email.Length == 0 || username.Length == 0 || password.Length == 0)
        {
            LoginModel lrm = new()
            {
                ErrorName = "Invalid username or password"
            };
            ViewBag.HideLinks = true;
            return View("Register", lrm);
        }
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Name == username || u.Email == email);

        if (existingUser != null)
        {
            LoginModel lrm = new()
            {
                ErrorName = "ERROR: Username or Email already exists"
            };
            ViewBag.HideLinks = true;
            return View("Register", lrm);
        }
        var user = new User { Email = email, Name = username, Password = BCrypt.Net.BCrypt.HashPassword(password), Role = "user" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return RedirectToAction("Login", "Login");
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

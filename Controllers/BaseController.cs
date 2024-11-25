using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SuppliChain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;


namespace SuppliChain.Controllers;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        string userName = HttpContext.Session.GetString("UserName");
        bool isAdmin = HttpContext.Session.GetString("IsAdmin") == "true";


        ViewBag.UserName = userName;
        ViewBag.IsAdmin = isAdmin;
    }
}
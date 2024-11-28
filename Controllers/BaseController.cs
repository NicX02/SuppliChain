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
        int loginToken = HttpContext.Session.GetInt32("UserId") ?? -1;


        ViewBag.UserName = userName;
        ViewBag.IsAdmin = isAdmin;
        ViewBag.LoginToken = loginToken;

        if (loginToken == -1)
        {
            var path = context.HttpContext.Request.Path;
            if (path != "/" && path != "/Login/Register")
                context.Result = Redirect("/");
        }
    }
}
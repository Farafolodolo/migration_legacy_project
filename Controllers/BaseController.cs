using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskManagerMVC.Controllers;

public class BaseController : Controller
{
    protected int CurrentUserId => HttpContext.Session.GetInt32("UserId") ?? 0;
    protected string CurrentUsername => HttpContext.Session.GetString("Username") ?? "";

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (HttpContext.Session.GetInt32("UserId") == null)
        {
            context.Result = RedirectToAction("Login", "Account");
            return;
        }

        ViewBag.CurrentUsername = CurrentUsername;
        ViewBag.CurrentUserId = CurrentUserId;
        base.OnActionExecuting(context);
    }
}

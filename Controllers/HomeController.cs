using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManagerMVC.Models;

namespace TaskManagerMVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        // Placeholder for user name until Identity is fully hooked up
        ViewBag.CurrentUsername = User.Identity?.IsAuthenticated == true ? User.Identity.Name : "Usuario";
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

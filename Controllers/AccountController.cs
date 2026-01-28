using Microsoft.AspNetCore.Mvc;
using TaskManagerMVC.Models.ViewModels;
using TaskManagerMVC.Services;

namespace TaskManagerMVC.Controllers;

public class AccountController : Controller
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        if (HttpContext.Session.GetInt32("UserId") != null)
        {
            return RedirectToAction("Index", "Tasks");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await _authService.ValidateUserAsync(model.Username, model.Password);
        if (user == null)
        {
            ModelState.AddModelError("", "Usuario o contraseña incorrectos");
            return View(model);
        }

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);

        return RedirectToAction("Index", "Tasks");
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (HttpContext.Session.GetInt32("UserId") != null)
        {
            return RedirectToAction("Index", "Tasks");
        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (await _authService.UsernameExistsAsync(model.Username))
        {
            ModelState.AddModelError("Username", "El nombre de usuario ya está en uso");
            return View(model);
        }

        if (await _authService.EmailExistsAsync(model.Email))
        {
            ModelState.AddModelError("Email", "El email ya está registrado");
            return View(model);
        }

        var user = await _authService.RegisterUserAsync(model.Username, model.Email, model.Password);
        if (user == null)
        {
            ModelState.AddModelError("", "Error al registrar el usuario");
            return View(model);
        }

        HttpContext.Session.SetInt32("UserId", user.Id);
        HttpContext.Session.SetString("Username", user.Username);

        return RedirectToAction("Index", "Tasks");
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}

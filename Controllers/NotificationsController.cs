using Microsoft.AspNetCore.Mvc;
using TaskManagerMVC.Services;

namespace TaskManagerMVC.Controllers;

public class NotificationsController : BaseController
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task<IActionResult> Index()
    {
        var notifications = await _notificationService.GetNotificationsByUserIdAsync(CurrentUserId);
        ViewBag.UnreadCount = await _notificationService.GetUnreadCountAsync(CurrentUserId);
        return View(notifications);
    }

    [HttpPost]
    public async Task<IActionResult> MarkAllAsRead()
    {
        await _notificationService.MarkAllAsReadAsync(CurrentUserId);
        TempData["Success"] = "Notificaciones marcadas como leídas";
        return RedirectToAction("Index");
    }
}

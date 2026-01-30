using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManagerMVC.Models;
using TaskManagerMVC.Models.Enums;
using TaskManagerMVC.Models.ViewModels;
using TaskManagerMVC.Services;

namespace TaskManagerMVC.Controllers;

public class HomeController : Controller
{
    private readonly ITaskService _taskService;
    private readonly IProjectService _projectService;

    public HomeController(ITaskService taskService, IProjectService projectService)
    {
        _taskService = taskService;
        _projectService = projectService;
    }

    public async Task<IActionResult> Index()
    {
        // 1. Fetch Data
        var allTasks = await _taskService.GetAllTasksAsync();
        var allProjects = await _projectService.GetAllProjectsAsync();

        // 2. Calculate Stats
        var activeProjectsCount = allProjects.Count;

        // "Tareas Pendientes" -> Status != Completed
        var pendingTasks = allTasks.Where(t => t.Status != TaskItemStatus.Completed).ToList();
        var pendingTasksCount = pendingTasks.Count;

        // "Urgentes" -> Priority == High && Status != Completed
        var urgentTasksCount = pendingTasks.Count(t => t.Priority == Priority.High);

        // "Completadas Hoy"
        var today = DateTime.UtcNow.Date;
        var completedTodayCount = allTasks.Count(t => 
            t.Status == TaskItemStatus.Completed && 
            t.UpdatedAt.HasValue && 
            t.UpdatedAt.Value.Date == today);

        // Recent Activity
        var recentActivity = allTasks
            .OrderByDescending(t => t.UpdatedAt ?? t.CreatedAt)
            .Take(5)
            .ToList();

        // 3. Prepare ViewModel
        var viewModel = new DashboardViewModel
        {
            ActiveProjectsCount = activeProjectsCount,
            PendingTasksCount = pendingTasksCount,
            UrgentTasksCount = urgentTasksCount,
            CompletedTodayCount = completedTodayCount,
            RecentActivity = recentActivity
        };

        // User requested explicitly to set this to "Usuario"
        ViewBag.CurrentUsername = "Usuario";

        return View(viewModel);
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

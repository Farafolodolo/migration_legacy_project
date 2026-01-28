using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagerMVC.Models;
using TaskManagerMVC.Models.ViewModels;
using TaskManagerMVC.Services;

namespace TaskManagerMVC.Controllers;

public class TasksController : BaseController
{
    private readonly ITaskService _taskService;
    private readonly IProjectService _projectService;
    private readonly IUserService _userService;

    public TasksController(ITaskService taskService, IProjectService projectService, IUserService userService)
    {
        _taskService = taskService;
        _projectService = projectService;
        _userService = userService;
    }

    public async Task<IActionResult> Index()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return View(new TaskListViewModel { Tasks = tasks });
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var model = new TaskCreateViewModel
        {
            Projects = new SelectList(await _projectService.GetAllProjectsAsync(), "Id", "Name"),
            Users = new SelectList(await _userService.GetAllUsersAsync(), "Id", "Username")
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TaskCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Projects = new SelectList(await _projectService.GetAllProjectsAsync(), "Id", "Name");
            model.Users = new SelectList(await _userService.GetAllUsersAsync(), "Id", "Username");
            return View(model);
        }

        var task = new TaskItem
        {
            Title = model.Title,
            Description = model.Description,
            Status = model.Status,
            Priority = model.Priority,
            DueDate = model.DueDate,
            EstimatedHours = model.EstimatedHours,
            ProjectId = model.ProjectId,
            AssignedToUserId = model.AssignedToUserId
        };

        await _taskService.CreateTaskAsync(task, CurrentUserId);
        TempData["Success"] = "Tarea creada exitosamente";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        var model = new TaskEditViewModel
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Status = task.Status,
            Priority = task.Priority,
            DueDate = task.DueDate,
            EstimatedHours = task.EstimatedHours,
            ProjectId = task.ProjectId,
            AssignedToUserId = task.AssignedToUserId,
            Projects = new SelectList(await _projectService.GetAllProjectsAsync(), "Id", "Name"),
            Users = new SelectList(await _userService.GetAllUsersAsync(), "Id", "Username")
        };

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(TaskEditViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Projects = new SelectList(await _projectService.GetAllProjectsAsync(), "Id", "Name");
            model.Users = new SelectList(await _userService.GetAllUsersAsync(), "Id", "Username");
            return View(model);
        }

        var task = new TaskItem
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            Status = model.Status,
            Priority = model.Priority,
            DueDate = model.DueDate,
            EstimatedHours = model.EstimatedHours,
            ProjectId = model.ProjectId,
            AssignedToUserId = model.AssignedToUserId
        };

        await _taskService.UpdateTaskAsync(task, CurrentUserId);
        TempData["Success"] = "Tarea actualizada exitosamente";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _taskService.DeleteTaskAsync(id, CurrentUserId);
        TempData["Success"] = "Tarea eliminada exitosamente";
        return RedirectToAction("Index");
    }
}

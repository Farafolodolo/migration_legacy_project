using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagerMVC.Models.ViewModels;
using TaskManagerMVC.Services;

namespace TaskManagerMVC.Controllers;

public class SearchController : BaseController
{
    private readonly ITaskService _taskService;
    private readonly IProjectService _projectService;

    public SearchController(ITaskService taskService, IProjectService projectService)
    {
        _taskService = taskService;
        _projectService = projectService;
    }

    public async Task<IActionResult> Index(TaskSearchViewModel model)
    {
        model.Projects = new SelectList(await _projectService.GetAllProjectsAsync(), "Id", "Name");

        if (Request.Query.Count > 0)
        {
            model.Results = await _taskService.SearchTasksAsync(
                model.Text,
                model.Status,
                model.Priority,
                model.ProjectId);
        }

        return View(model);
    }
}

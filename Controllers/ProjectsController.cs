using Microsoft.AspNetCore.Mvc;
using TaskManagerMVC.Models;
using TaskManagerMVC.Services;

namespace TaskManagerMVC.Controllers;

public class ProjectsController : BaseController
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        return View(projects);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new Project());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Project project)
    {
        if (!ModelState.IsValid)
        {
            return View(project);
        }

        await _projectService.CreateProjectAsync(project);
        TempData["Success"] = "Proyecto creado exitosamente";
        return RedirectToAction("Index");
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Project project)
    {
        if (!ModelState.IsValid)
        {
            return View(project);
        }

        await _projectService.UpdateProjectAsync(project);
        TempData["Success"] = "Proyecto actualizado exitosamente";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        await _projectService.DeleteProjectAsync(id);
        TempData["Success"] = "Proyecto eliminado exitosamente";
        return RedirectToAction("Index");
    }
}

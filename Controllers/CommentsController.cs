using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagerMVC.Services;

namespace TaskManagerMVC.Controllers;

public class CommentsController : BaseController
{
    private readonly ICommentService _commentService;
    private readonly ITaskService _taskService;

    public CommentsController(ICommentService commentService, ITaskService taskService)
    {
        _commentService = commentService;
        _taskService = taskService;
    }

    public async Task<IActionResult> Index(int? taskId)
    {
        var tasks = await _taskService.GetAllTasksAsync();
        ViewBag.Tasks = new SelectList(tasks, "Id", "Title");
        ViewBag.SelectedTaskId = taskId;

        if (taskId.HasValue)
        {
            var comments = await _commentService.GetCommentsByTaskIdAsync(taskId.Value);
            var task = await _taskService.GetTaskByIdAsync(taskId.Value);
            ViewBag.TaskTitle = task?.Title;
            return View(comments);
        }

        return View(new List<TaskManagerMVC.Models.Comment>());
    }

    [HttpPost]
    public async Task<IActionResult> Add(int taskId, string content)
    {
        if (string.IsNullOrWhiteSpace(content))
        {
            TempData["Error"] = "El comentario no puede estar vacío";
            return RedirectToAction("Index", new { taskId });
        }

        await _commentService.AddCommentAsync(taskId, CurrentUserId, content);
        TempData["Success"] = "Comentario agregado exitosamente";
        return RedirectToAction("Index", new { taskId });
    }
}

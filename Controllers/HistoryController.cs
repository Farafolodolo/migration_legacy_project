using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagerMVC.Services;

namespace TaskManagerMVC.Controllers;

public class HistoryController : BaseController
{
    private readonly IHistoryService _historyService;
    private readonly ITaskService _taskService;

    public HistoryController(IHistoryService historyService, ITaskService taskService)
    {
        _historyService = historyService;
        _taskService = taskService;
    }

    public async Task<IActionResult> Index(int? taskId)
    {
        var tasks = await _taskService.GetAllTasksAsync();
        ViewBag.Tasks = new SelectList(tasks, "Id", "Title");
        ViewBag.SelectedTaskId = taskId;

        if (taskId.HasValue)
        {
            var history = await _historyService.GetHistoryByTaskIdAsync(taskId.Value);
            var task = await _taskService.GetTaskByIdAsync(taskId.Value);
            ViewBag.TaskTitle = task?.Title;
            return View(history);
        }

        var allHistory = await _historyService.GetAllHistoryAsync();
        return View(allHistory);
    }
}

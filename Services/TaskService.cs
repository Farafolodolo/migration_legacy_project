using Microsoft.EntityFrameworkCore;
using TaskManagerMVC.Data;
using TaskManagerMVC.Models;
using TaskManagerMVC.Models.Enums;

namespace TaskManagerMVC.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetAllTasksAsync();
    Task<TaskItem?> GetTaskByIdAsync(int id);
    Task<TaskItem> CreateTaskAsync(TaskItem task, int userId);
    Task<TaskItem?> UpdateTaskAsync(TaskItem task, int userId);
    Task<bool> DeleteTaskAsync(int id, int userId);
    Task<List<TaskItem>> SearchTasksAsync(string? text, TaskItemStatus? status, Priority? priority, int? projectId);
}

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;
    private readonly IHistoryService _historyService;

    public TaskService(ApplicationDbContext context, IHistoryService historyService)
    {
        _context = context;
        _historyService = historyService;
    }

    public async Task<List<TaskItem>> GetAllTasksAsync()
    {
        return await _context.TaskItems
            .Include(t => t.Project)
            .Include(t => t.AssignedToUser)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<TaskItem?> GetTaskByIdAsync(int id)
    {
        return await _context.TaskItems
            .Include(t => t.Project)
            .Include(t => t.AssignedToUser)
            .Include(t => t.CreatedByUser)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> CreateTaskAsync(TaskItem task, int userId)
    {
        task.CreatedByUserId = userId;
        task.CreatedAt = DateTime.UtcNow;
        task.DueDate = task.DueDate.Value.ToUniversalTime();

        _context.TaskItems.Add(task);
        await _context.SaveChangesAsync();

        await _historyService.RecordHistoryAsync(task.Id, userId, HistoryActionType.Created, null, task.Title);

        return task;
    }

    public async Task<TaskItem?> UpdateTaskAsync(TaskItem updatedTask, int userId)
    {
        var existingTask = await _context.TaskItems.FindAsync(updatedTask.Id);
        if (existingTask == null) return null;

        // Record changes
        if (existingTask.Title != updatedTask.Title)
            await _historyService.RecordHistoryAsync(existingTask.Id, userId, HistoryActionType.TitleChanged, existingTask.Title, updatedTask.Title);
        if (existingTask.Description != updatedTask.Description)
            await _historyService.RecordHistoryAsync(existingTask.Id, userId, HistoryActionType.DescriptionChanged, existingTask.Description, updatedTask.Description);
        if (existingTask.Status != updatedTask.Status)
            await _historyService.RecordHistoryAsync(existingTask.Id, userId, HistoryActionType.StatusChanged, existingTask.Status.ToString(), updatedTask.Status.ToString());
        if (existingTask.Priority != updatedTask.Priority)
            await _historyService.RecordHistoryAsync(existingTask.Id, userId, HistoryActionType.PriorityChanged, existingTask.Priority.ToString(), updatedTask.Priority.ToString());
        if (existingTask.ProjectId != updatedTask.ProjectId)
            await _historyService.RecordHistoryAsync(existingTask.Id, userId, HistoryActionType.ProjectChanged, existingTask.ProjectId.ToString(), updatedTask.ProjectId.ToString());
        if (existingTask.AssignedToUserId != updatedTask.AssignedToUserId)
            await _historyService.RecordHistoryAsync(existingTask.Id, userId, HistoryActionType.AssigneeChanged, existingTask.AssignedToUserId?.ToString(), updatedTask.AssignedToUserId?.ToString());

        existingTask.Title = updatedTask.Title;
        existingTask.Description = updatedTask.Description;
        existingTask.Status = updatedTask.Status;
        existingTask.Priority = updatedTask.Priority;
        existingTask.DueDate = updatedTask.DueDate.Value.ToUniversalTime();
        existingTask.EstimatedHours = updatedTask.EstimatedHours;
        existingTask.ProjectId = updatedTask.ProjectId;
        existingTask.AssignedToUserId = updatedTask.AssignedToUserId;
        existingTask.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();
        return existingTask;
    }

    public async Task<bool> DeleteTaskAsync(int id, int userId)
    {
        var task = await _context.TaskItems.FindAsync(id);
        if (task == null) return false;

        await _historyService.RecordHistoryAsync(id, userId, HistoryActionType.Deleted, task.Title, null);

        _context.TaskItems.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<TaskItem>> SearchTasksAsync(string? text, TaskItemStatus? status, Priority? priority, int? projectId)
    {
        var query = _context.TaskItems
            .Include(t => t.Project)
            .Include(t => t.AssignedToUser)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(text))
        {
            query = query.Where(t => t.Title.Contains(text) || t.Description.Contains(text));
        }

        if (status.HasValue)
        {
            query = query.Where(t => t.Status == status.Value);
        }

        if (priority.HasValue)
        {
            query = query.Where(t => t.Priority == priority.Value);
        }

        if (projectId.HasValue)
        {
            query = query.Where(t => t.ProjectId == projectId.Value);
        }

        return await query.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }
}

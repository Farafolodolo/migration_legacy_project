using Microsoft.EntityFrameworkCore;
using TaskManagerMVC.Data;
using TaskManagerMVC.Models;
using TaskManagerMVC.Models.Enums;

namespace TaskManagerMVC.Services;

public interface IHistoryService
{
    Task<List<TaskHistory>> GetHistoryByTaskIdAsync(int taskId);
    Task<List<TaskHistory>> GetAllHistoryAsync();
    Task RecordHistoryAsync(int taskId, int userId, HistoryActionType actionType, string? oldValue, string? newValue);
}

public class HistoryService : IHistoryService
{
    private readonly ApplicationDbContext _context;

    public HistoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskHistory>> GetHistoryByTaskIdAsync(int taskId)
    {
        return await _context.TaskHistories
            .Include(h => h.User)
            .Where(h => h.TaskItemId == taskId)
            .OrderByDescending(h => h.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<TaskHistory>> GetAllHistoryAsync()
    {
        return await _context.TaskHistories
            .Include(h => h.User)
            .Include(h => h.TaskItem)
            .OrderByDescending(h => h.CreatedAt)
            .Take(100)
            .ToListAsync();
    }

    public async Task RecordHistoryAsync(int taskId, int userId, HistoryActionType actionType, string? oldValue, string? newValue)
    {
        var history = new TaskHistory
        {
            TaskItemId = taskId,
            UserId = userId,
            ActionType = actionType,
            OldValue = oldValue,
            NewValue = newValue,
            CreatedAt = DateTime.UtcNow
        };

        _context.TaskHistories.Add(history);
        await _context.SaveChangesAsync();
    }
}

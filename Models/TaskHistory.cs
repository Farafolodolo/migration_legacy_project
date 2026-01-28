using TaskManagerMVC.Models.Enums;

namespace TaskManagerMVC.Models;

public class TaskHistory
{
    public int Id { get; set; }
    public HistoryActionType ActionType { get; set; }
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign keys
    public int TaskItemId { get; set; }
    public int UserId { get; set; }

    // Navigation properties
    public TaskItem TaskItem { get; set; } = null!;
    public User User { get; set; } = null!;
}

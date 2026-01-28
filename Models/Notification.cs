namespace TaskManagerMVC.Models;

public class Notification
{
    public int Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign keys
    public int UserId { get; set; }
    public int? TaskItemId { get; set; }

    // Navigation properties
    public User User { get; set; } = null!;
    public TaskItem? TaskItem { get; set; }
}

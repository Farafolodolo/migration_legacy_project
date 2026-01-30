using TaskManagerMVC.Models.Enums;

namespace TaskManagerMVC.Models.ViewModels;

public class DashboardViewModel
{
    public int ActiveProjectsCount { get; set; }
    public int PendingTasksCount { get; set; }
    public int UrgentTasksCount { get; set; }
    public int CompletedTodayCount { get; set; }
    public List<TaskItem> RecentActivity { get; set; } = new();
}

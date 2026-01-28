namespace TaskManagerMVC.Models.ViewModels;

public class TaskReportViewModel
{
    public int TotalTasks { get; set; }
    public int PendingTasks { get; set; }
    public int InProgressTasks { get; set; }
    public int CompletedTasks { get; set; }
}

public class ProjectReportViewModel
{
    public int TotalProjects { get; set; }
    public List<ProjectTaskCount> ProjectsWithTaskCount { get; set; } = new();
}

public class ProjectTaskCount
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public int TaskCount { get; set; }
}

public class UserReportViewModel
{
    public int TotalUsers { get; set; }
    public List<UserTaskCount> UsersWithTaskCount { get; set; } = new();
}

public class UserTaskCount
{
    public int UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public int AssignedTaskCount { get; set; }
}

public class ReportsViewModel
{
    public TaskReportViewModel? TaskReport { get; set; }
    public ProjectReportViewModel? ProjectReport { get; set; }
    public UserReportViewModel? UserReport { get; set; }
    public string? ReportType { get; set; }
}

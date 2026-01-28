using Microsoft.EntityFrameworkCore;
using TaskManagerMVC.Data;
using TaskManagerMVC.Models.Enums;
using TaskManagerMVC.Models.ViewModels;
using System.Text;

namespace TaskManagerMVC.Services;

public interface IReportService
{
    Task<TaskReportViewModel> GetTaskReportAsync();
    Task<ProjectReportViewModel> GetProjectReportAsync();
    Task<UserReportViewModel> GetUserReportAsync();
    Task<string> GenerateCsvReportAsync();
}

public class ReportService : IReportService
{
    private readonly ApplicationDbContext _context;

    public ReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<TaskReportViewModel> GetTaskReportAsync()
    {
        var tasks = await _context.TaskItems.ToListAsync();

        return new TaskReportViewModel
        {
            TotalTasks = tasks.Count,
            PendingTasks = tasks.Count(t => t.Status == TaskItemStatus.Pending),
            InProgressTasks = tasks.Count(t => t.Status == TaskItemStatus.InProgress),
            CompletedTasks = tasks.Count(t => t.Status == TaskItemStatus.Completed)
        };
    }

    public async Task<ProjectReportViewModel> GetProjectReportAsync()
    {
        var projects = await _context.Projects
            .Where(p => p.IsActive)
            .Select(p => new ProjectTaskCount
            {
                ProjectId = p.Id,
                ProjectName = p.Name,
                TaskCount = p.Tasks.Count
            })
            .ToListAsync();

        return new ProjectReportViewModel
        {
            TotalProjects = projects.Count,
            ProjectsWithTaskCount = projects
        };
    }

    public async Task<UserReportViewModel> GetUserReportAsync()
    {
        var users = await _context.Users
            .Where(u => u.IsActive)
            .Select(u => new UserTaskCount
            {
                UserId = u.Id,
                Username = u.Username,
                AssignedTaskCount = u.AssignedTasks.Count
            })
            .ToListAsync();

        return new UserReportViewModel
        {
            TotalUsers = users.Count,
            UsersWithTaskCount = users
        };
    }

    public async Task<string> GenerateCsvReportAsync()
    {
        var tasks = await _context.TaskItems
            .Include(t => t.Project)
            .Include(t => t.AssignedToUser)
            .ToListAsync();

        var sb = new StringBuilder();
        sb.AppendLine("ID,Titulo,Estado,Prioridad,Proyecto,AsignadoA,FechaVencimiento,HorasEstimadas");

        foreach (var task in tasks)
        {
            var status = task.Status switch
            {
                TaskItemStatus.Pending => "Pendiente",
                TaskItemStatus.InProgress => "En Progreso",
                TaskItemStatus.Completed => "Completada",
                _ => task.Status.ToString()
            };

            var priority = task.Priority switch
            {
                Priority.Low => "Baja",
                Priority.Medium => "Media",
                Priority.High => "Alta",
                _ => task.Priority.ToString()
            };

            sb.AppendLine($"{task.Id},\"{task.Title}\",{status},{priority},\"{task.Project?.Name}\",\"{task.AssignedToUser?.Username ?? "Sin asignar"}\",{task.DueDate?.ToString("yyyy-MM-dd") ?? ""},{ task.EstimatedHours?.ToString() ?? ""}");
        }

        return sb.ToString();
    }
}

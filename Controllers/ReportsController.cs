using Microsoft.AspNetCore.Mvc;
using TaskManagerMVC.Models.ViewModels;
using TaskManagerMVC.Services;
using System.Text;

namespace TaskManagerMVC.Controllers;

public class ReportsController : BaseController
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    public async Task<IActionResult> Index()
    {
        // Cargar reporte de tareas por defecto
        var report = await _reportService.GetTaskReportAsync();
        return View(new ReportsViewModel { TaskReport = report, ReportType = "tasks" });
    }

    [HttpPost]
    public async Task<IActionResult> GenerateTaskReport()
    {
        var report = await _reportService.GetTaskReportAsync();
        return View("Index", new ReportsViewModel { TaskReport = report, ReportType = "tasks" });
    }

    [HttpPost]
    public async Task<IActionResult> GenerateProjectReport()
    {
        var report = await _reportService.GetProjectReportAsync();
        return View("Index", new ReportsViewModel { ProjectReport = report, ReportType = "projects" });
    }

    [HttpPost]
    public async Task<IActionResult> GenerateUserReport()
    {
        var report = await _reportService.GetUserReportAsync();
        return View("Index", new ReportsViewModel { UserReport = report, ReportType = "users" });
    }

    [HttpPost]
    public async Task<IActionResult> ExportCsv(string reportType)
    {
        string csv;
        string filename;

        switch (reportType)
        {
            case "projects":
                csv = await _reportService.GenerateProjectCsvAsync();
                filename = "proyectos_export.csv";
                break;
            case "users":
                csv = await _reportService.GenerateUserCsvAsync();
                filename = "usuarios_export.csv";
                break;
            case "tasks":
            default:
                csv = await _reportService.GenerateCsvReportAsync();
                filename = "tareas_export.csv";
                break;
        }

        var bytes = Encoding.UTF8.GetBytes(csv);
        return File(bytes, "text/csv", filename);
    }
}

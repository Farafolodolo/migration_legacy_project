using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManagerMVC.Models.Enums;

namespace TaskManagerMVC.Models.ViewModels;

public class TaskCreateViewModel
{
    [Required(ErrorMessage = "El título es requerido")]
    [StringLength(200)]
    [Display(Name = "Título")]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(2000)]
    [Display(Name = "Descripción")]
    public string Description { get; set; } = string.Empty;
    
    [Display(Name = "Estado")]
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;
    
    [Display(Name = "Prioridad")]
    public Priority Priority { get; set; } = Priority.Medium;
    
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Vencimiento")]
    public DateTime? DueDate { get; set; }
    
    [Display(Name = "Horas Estimadas")]
    [Range(0, 9999)]
    public decimal? EstimatedHours { get; set; }
    
    [Required(ErrorMessage = "El proyecto es requerido")]
    [Display(Name = "Proyecto")]
    public int ProjectId { get; set; }
    
    [Display(Name = "Asignado a")]
    public int? AssignedToUserId { get; set; }
    
    public SelectList? Projects { get; set; }
    public SelectList? Users { get; set; }
}

public class TaskEditViewModel : TaskCreateViewModel
{
    public int Id { get; set; }
}

public class TaskSearchViewModel
{
    [Display(Name = "Texto")]
    public string? Text { get; set; }
    
    [Display(Name = "Estado")]
    public TaskItemStatus? Status { get; set; }
    
    [Display(Name = "Prioridad")]
    public Priority? Priority { get; set; }
    
    [Display(Name = "Proyecto")]
    public int? ProjectId { get; set; }
    
    public List<TaskItem> Results { get; set; } = new();
    public SelectList? Projects { get; set; }
}

public class TaskListViewModel
{
    public List<TaskItem> Tasks { get; set; } = new();
}

using System.ComponentModel.DataAnnotations;
using TaskManagerMVC.Models.Enums;

namespace TaskManagerMVC.Models;

public class TaskItem
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El título es requerido")]
    [StringLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
    public string Title { get; set; } = string.Empty;
    
    [StringLength(2000, ErrorMessage = "La descripción no puede exceder 2000 caracteres")]
    public string Description { get; set; } = string.Empty;
    
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;
    public Priority Priority { get; set; } = Priority.Medium;
    
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Vencimiento")]
    public DateTime? DueDate { get; set; }
    
    [Display(Name = "Horas Estimadas")]
    [Range(0, 9999, ErrorMessage = "Las horas deben ser entre 0 y 9999")]
    public decimal? EstimatedHours { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Foreign keys
    [Required(ErrorMessage = "El proyecto es requerido")]
    public int ProjectId { get; set; }
    public int CreatedByUserId { get; set; }
    public int? AssignedToUserId { get; set; }

    // Navigation properties
    public Project Project { get; set; } = null!;
    public User CreatedByUser { get; set; } = null!;
    public User? AssignedToUser { get; set; }
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<TaskHistory> History { get; set; } = new List<TaskHistory>();
}

using System.ComponentModel.DataAnnotations;

namespace TaskManagerMVC.Models;

public class Comment
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "El contenido es requerido")]
    [StringLength(1000, ErrorMessage = "El comentario no puede exceder 1000 caracteres")]
    public string Content { get; set; } = string.Empty;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Foreign keys
    public int TaskItemId { get; set; }
    public int UserId { get; set; }

    // Navigation properties
    public TaskItem TaskItem { get; set; } = null!;
    public User User { get; set; } = null!;
}

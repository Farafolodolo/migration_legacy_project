using Microsoft.EntityFrameworkCore;
using TaskManagerMVC.Data;
using TaskManagerMVC.Models;

namespace TaskManagerMVC.Services;

public interface ICommentService
{
    Task<List<Comment>> GetCommentsByTaskIdAsync(int taskId);
    Task<Comment> AddCommentAsync(int taskId, int userId, string content);
}

public class CommentService : ICommentService
{
    private readonly ApplicationDbContext _context;

    public CommentService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetCommentsByTaskIdAsync(int taskId)
    {
        return await _context.Comments
            .Include(c => c.User)
            .Where(c => c.TaskItemId == taskId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<Comment> AddCommentAsync(int taskId, int userId, string content)
    {
        var comment = new Comment
        {
            TaskItemId = taskId,
            UserId = userId,
            Content = content,
            CreatedAt = DateTime.UtcNow
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        // Reload with user
        await _context.Entry(comment).Reference(c => c.User).LoadAsync();

        return comment;
    }
}

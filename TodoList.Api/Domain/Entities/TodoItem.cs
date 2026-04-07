namespace TodoList.Api.Domain.Entities;

using TodoList.Api.Domain.Enums;

public class TodoItem : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    public DateTime? DueDateUtc { get; set; }

    public int UserId { get; set; }
    public ApplicationUser User { get; set; } = null!;
}

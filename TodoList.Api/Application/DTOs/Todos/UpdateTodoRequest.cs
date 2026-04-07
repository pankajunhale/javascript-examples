using TodoList.Api.Domain.Enums;

namespace TodoList.Api.Application.DTOs.Todos;

public sealed class UpdateTodoRequest
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    public DateTime? DueDateUtc { get; set; }
}

using TodoList.Api.Domain.Enums;

namespace TodoList.Api.Application.DTOs.Todos;

public sealed class TodoResponse
{
    public int Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string? Description { get; init; }
    public bool IsCompleted { get; init; }
    public TodoPriority Priority { get; init; }
    public DateTime? DueDateUtc { get; init; }
    public DateTime CreatedAtUtc { get; init; }
    public DateTime UpdatedAtUtc { get; init; }
}

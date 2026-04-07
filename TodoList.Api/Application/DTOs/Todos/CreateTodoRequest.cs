using System.ComponentModel.DataAnnotations;
using TodoList.Api.Domain.Enums;

namespace TodoList.Api.Application.DTOs.Todos;

public sealed class CreateTodoRequest
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
    public DateTime? DueDateUtc { get; set; }
}

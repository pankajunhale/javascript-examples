namespace TodoList.Api.Domain.Entities;

public class ApplicationUser : BaseEntity
{
    public required string FullName { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public required string PasswordSalt { get; set; }

    public ICollection<TodoItem> TodoItems { get; set; } = new List<TodoItem>();
}

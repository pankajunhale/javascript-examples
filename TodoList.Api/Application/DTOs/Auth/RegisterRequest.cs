using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Application.DTOs.Auth;

public sealed class RegisterRequest
{
    [Required]
    [MaxLength(100)]
    public required string FullName { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Username { get; set; }

    [Required]
    [EmailAddress]
    [MaxLength(100)]
    public required string Email { get; set; }

    [Required]
    [MinLength(8)]
    public required string Password { get; set; }
}

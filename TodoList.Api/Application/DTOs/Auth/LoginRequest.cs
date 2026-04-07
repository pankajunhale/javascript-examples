using System.ComponentModel.DataAnnotations;

namespace TodoList.Api.Application.DTOs.Auth;

public sealed class LoginRequest
{
    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; set; } = string.Empty;
}

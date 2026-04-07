namespace TodoList.Api.Application.DTOs.Auth;

public sealed record AuthResponse(
    string AccessToken,
    int UserId,
    string Username,
    string FullName,
    string Email);

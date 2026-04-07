namespace TodoList.Api.Application.Exceptions;

public sealed class UnauthorizedException(string message)
    : AppException(message, System.Net.HttpStatusCode.Unauthorized);

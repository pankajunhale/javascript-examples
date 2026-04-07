namespace TodoList.Api.Application.Exceptions;

public sealed class NotFoundException(string message) : AppException(message, System.Net.HttpStatusCode.NotFound);

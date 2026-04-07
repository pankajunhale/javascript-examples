namespace TodoList.Api.Application.Exceptions;

public sealed class ValidationException(string message) : AppException(message, System.Net.HttpStatusCode.BadRequest);

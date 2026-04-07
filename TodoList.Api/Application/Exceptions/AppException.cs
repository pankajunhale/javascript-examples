using System.Net;

namespace TodoList.Api.Application.Exceptions;

public abstract class AppException(string message, HttpStatusCode statusCode) : Exception(message)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
}

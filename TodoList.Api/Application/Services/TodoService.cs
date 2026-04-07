using TodoList.Api.Application.Common;
using TodoList.Api.Application.DTOs.Todos;
using TodoList.Api.Application.Exceptions;
using TodoList.Api.Application.Interfaces;
using TodoList.Api.Application.Interfaces.Services;
using TodoList.Api.Domain.Entities;

namespace TodoList.Api.Application.Services;

public sealed class TodoService(IUnitOfWork unitOfWork) : ITodoService
{
    public async Task<PaginatedResult<TodoResponse>> GetPagedForUserAsync(
        int userId,
        PaginationQuery query,
        CancellationToken cancellationToken = default)
    {
        var pageNumber = query.PageNumber < 1 ? 1 : query.PageNumber;
        var pageSize = query.PageSize < 1 ? 10 : query.PageSize;

        var (items, totalCount) = await unitOfWork.TodoRepository.GetPagedByUserAsync(
            userId,
            pageNumber,
            pageSize,
            cancellationToken);

        return new PaginatedResult<TodoResponse>
        {
            Items = items.Select(MapToResponse).ToArray(),
            TotalCount = totalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<TodoResponse> GetByIdAsync(
        int id,
        int userId,
        CancellationToken cancellationToken = default)
    {
        var todo = await unitOfWork.TodoRepository.GetByIdForUserAsync(id, userId, cancellationToken)
            ?? throw new NotFoundException("Todo item not found.");

        return MapToResponse(todo);
    }

    public async Task<TodoResponse> CreateAsync(
        CreateTodoRequest request,
        int userId,
        CancellationToken cancellationToken = default)
    {
        ValidateCreateRequest(request);

        var todoItem = new TodoItem
        {
            Title = request.Title.Trim(),
            Description = request.Description?.Trim(),
            Priority = request.Priority,
            DueDateUtc = request.DueDateUtc,
            UserId = userId
        };

        await unitOfWork.TodoRepository.AddAsync(todoItem, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToResponse(todoItem);
    }

    public async Task<TodoResponse> UpdateAsync(
        int id,
        UpdateTodoRequest request,
        int userId,
        CancellationToken cancellationToken = default)
    {
        ValidateUpdateRequest(request);

        var todo = await unitOfWork.TodoRepository.GetByIdForUserAsync(id, userId, cancellationToken)
            ?? throw new NotFoundException("Todo item not found.");

        todo.Title = request.Title.Trim();
        todo.Description = request.Description?.Trim();
        todo.Priority = request.Priority;
        todo.DueDateUtc = request.DueDateUtc;
        todo.IsCompleted = request.IsCompleted;

        unitOfWork.TodoRepository.Update(todo);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return MapToResponse(todo);
    }

    public async Task DeleteAsync(int id, int userId, CancellationToken cancellationToken = default)
    {
        var todo = await unitOfWork.TodoRepository.GetByIdForUserAsync(id, userId, cancellationToken)
            ?? throw new NotFoundException("Todo item not found.");

        unitOfWork.TodoRepository.Delete(todo);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private static void ValidateCreateRequest(CreateTodoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ValidationException("Title is required.");
        }

        if (request.Title.Length > 200)
        {
            throw new ValidationException("Title cannot exceed 200 characters.");
        }
    }

    private static void ValidateUpdateRequest(UpdateTodoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            throw new ValidationException("Title is required.");
        }

        if (request.Title.Length > 200)
        {
            throw new ValidationException("Title cannot exceed 200 characters.");
        }

    }

    private static TodoResponse MapToResponse(TodoItem item)
    {
        return new TodoResponse
        {
            Id = item.Id,
            Title = item.Title,
            Description = item.Description,
            IsCompleted = item.IsCompleted,
            Priority = item.Priority,
            DueDateUtc = item.DueDateUtc,
            CreatedAtUtc = item.CreatedAtUtc,
            UpdatedAtUtc = item.UpdatedAtUtc
        };
    }
}

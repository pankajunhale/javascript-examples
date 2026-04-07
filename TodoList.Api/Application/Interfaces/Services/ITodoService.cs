using TodoList.Api.Application.Common;
using TodoList.Api.Application.DTOs.Todos;

namespace TodoList.Api.Application.Interfaces.Services;

public interface ITodoService
{
    Task<PaginatedResult<TodoResponse>> GetPagedForUserAsync(
        int userId,
        PaginationQuery query,
        CancellationToken cancellationToken = default);

    Task<TodoResponse> GetByIdAsync(int id, int userId, CancellationToken cancellationToken = default);
    Task<TodoResponse> CreateAsync(CreateTodoRequest request, int userId, CancellationToken cancellationToken = default);
    Task<TodoResponse> UpdateAsync(int id, UpdateTodoRequest request, int userId, CancellationToken cancellationToken = default);
    Task DeleteAsync(int id, int userId, CancellationToken cancellationToken = default);
}

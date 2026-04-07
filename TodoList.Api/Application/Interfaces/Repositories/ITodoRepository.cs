using TodoList.Api.Domain.Entities;

namespace TodoList.Api.Application.Interfaces.Repositories;

public interface ITodoRepository : IGenericRepository<TodoItem>
{
    Task<(IReadOnlyCollection<TodoItem> Items, int TotalCount)> GetPagedByUserAsync(
        int userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<TodoItem?> GetByIdForUserAsync(int id, int userId, CancellationToken cancellationToken = default);
}

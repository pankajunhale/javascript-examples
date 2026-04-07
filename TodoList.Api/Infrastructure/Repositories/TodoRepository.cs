using Microsoft.EntityFrameworkCore;
using TodoList.Api.Application.Interfaces.Repositories;
using TodoList.Api.Domain.Entities;
using TodoList.Api.Infrastructure.Data;

namespace TodoList.Api.Infrastructure.Repositories;

public sealed class TodoRepository(ApplicationDbContext dbContext)
    : GenericRepository<TodoItem>(dbContext), ITodoRepository
{
    public async Task<(IReadOnlyCollection<TodoItem> Items, int TotalCount)> GetPagedByUserAsync(
        int userId,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = DbContext.TodoItems
            .AsNoTracking()
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAtUtc);

        var totalCount = await query.CountAsync(cancellationToken);
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return (items, totalCount);
    }

    public async Task<TodoItem?> GetByIdForUserAsync(int id, int userId, CancellationToken cancellationToken = default)
    {
        return await DbContext.TodoItems
            .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId, cancellationToken);
    }
}

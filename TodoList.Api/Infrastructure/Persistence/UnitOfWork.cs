using TodoList.Api.Application.Interfaces;
using TodoList.Api.Application.Interfaces.Repositories;
using TodoList.Api.Infrastructure.Data;
using TodoList.Api.Infrastructure.Repositories;

namespace TodoList.Api.Infrastructure.Persistence;

public sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    private ITodoRepository? _todoRepository;
    private IUserRepository? _userRepository;

    public ITodoRepository TodoRepository => _todoRepository ??= new TodoRepository(dbContext);
    public IUserRepository UserRepository => _userRepository ??= new UserRepository(dbContext);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}

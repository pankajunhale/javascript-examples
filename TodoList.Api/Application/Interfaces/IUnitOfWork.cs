using TodoList.Api.Application.Interfaces.Repositories;

namespace TodoList.Api.Application.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    ITodoRepository TodoRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

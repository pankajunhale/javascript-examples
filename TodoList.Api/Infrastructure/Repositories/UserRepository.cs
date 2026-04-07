using Microsoft.EntityFrameworkCore;
using TodoList.Api.Application.Interfaces.Repositories;
using TodoList.Api.Domain.Entities;
using TodoList.Api.Infrastructure.Data;

namespace TodoList.Api.Infrastructure.Repositories;

public sealed class UserRepository(ApplicationDbContext dbContext)
    : GenericRepository<ApplicationUser>(dbContext), IUserRepository
{
    public async Task<ApplicationUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await DbContext.Users
            .FirstOrDefaultAsync(u => u.Username == username, cancellationToken);
    }

    public async Task<bool> ExistsByUsernameAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Users
            .AnyAsync(u => u.Username == username, cancellationToken);
    }

    public async Task<bool> ExistsByEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Users
            .AnyAsync(u => u.Email == email, cancellationToken);
    }
}

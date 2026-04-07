using TodoList.Api.Domain.Entities;

namespace TodoList.Api.Application.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(ApplicationUser user);
}

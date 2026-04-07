namespace TodoList.Api.Application.Interfaces.Services;

public interface IPasswordHasher
{
    (string PasswordHash, string PasswordSalt) HashPassword(string password);
    bool VerifyPassword(string password, string hash, string salt);
}

using TodoList.Api.Application.DTOs.Auth;
using TodoList.Api.Application.Exceptions;
using TodoList.Api.Application.Interfaces;
using TodoList.Api.Application.Interfaces.Services;
using TodoList.Api.Domain.Entities;

namespace TodoList.Api.Application.Services;

public sealed class AuthService(
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    ITokenService tokenService) : IAuthService
{
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        ValidateRegisterRequest(request);

        var normalizedUsername = request.Username.Trim().ToLowerInvariant();
        var normalizedEmail = request.Email.Trim().ToLowerInvariant();

        var usernameExists = await unitOfWork.UserRepository.ExistsByUsernameAsync(normalizedUsername, cancellationToken);
        var emailExists = await unitOfWork.UserRepository.ExistsByEmailAsync(normalizedEmail, cancellationToken);
        if (usernameExists || emailExists)
        {
            throw new ValidationException("Username or email is already in use.");
        }

        var (passwordHash, passwordSalt) = passwordHasher.HashPassword(request.Password);
        var user = new ApplicationUser
        {
            FullName = request.FullName.Trim(),
            Username = normalizedUsername,
            Email = normalizedEmail,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await unitOfWork.UserRepository.AddAsync(user, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        var token = tokenService.GenerateToken(user);
        return new AuthResponse(token, user.Id, user.Username, user.FullName, user.Email);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ValidationException("Username and password are required.");
        }

        var user = await unitOfWork.UserRepository.GetByUsernameAsync(
            request.Username.Trim().ToLowerInvariant(),
            cancellationToken);

        if (user is null || !passwordHasher.VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            throw new UnauthorizedException("Invalid username or password.");
        }

        var token = tokenService.GenerateToken(user);
        return new AuthResponse(token, user.Id, user.Username, user.FullName, user.Email);
    }

    private static void ValidateRegisterRequest(RegisterRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.FullName))
        {
            throw new ValidationException("Full name is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Username))
        {
            throw new ValidationException("Username is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ValidationException("Email is required.");
        }

        if (!request.Email.Contains('@'))
        {
            throw new ValidationException("A valid email is required.");
        }

        if (string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 8)
        {
            throw new ValidationException("Password must be at least 8 characters.");
        }
    }
}

using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using TodoList.Api.Application.Interfaces.Services;

namespace TodoList.Api.Application.Services;

public sealed class PasswordHasher : IPasswordHasher
{
    private const int IterationCount = 100_000;
    private const int SaltSize = 16;
    private const int KeySize = 32;

    public (string PasswordHash, string PasswordSalt) HashPassword(string password)
    {
        var saltBytes = RandomNumberGenerator.GetBytes(SaltSize);
        var hashBytes = KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: IterationCount,
            numBytesRequested: KeySize);

        return (Convert.ToBase64String(hashBytes), Convert.ToBase64String(saltBytes));
    }

    public bool VerifyPassword(string password, string hash, string salt)
    {
        var saltBytes = Convert.FromBase64String(salt);
        var computedHash = KeyDerivation.Pbkdf2(
            password: password,
            salt: saltBytes,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: IterationCount,
            numBytesRequested: KeySize);

        return CryptographicOperations.FixedTimeEquals(computedHash, Convert.FromBase64String(hash));
    }
}

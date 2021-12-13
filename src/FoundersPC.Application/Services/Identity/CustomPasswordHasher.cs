#region Using namespaces

using FoundersPC.Domain.Entities.Identity.Users;
using Microsoft.AspNetCore.Identity;

#endregion

namespace FoundersPC.Application.Services.Identity;

public class CustomPasswordHasher : PasswordHasher<ApplicationUser>
{
    private readonly PasswordEncryptorService _passwordEncryptorService;

    public CustomPasswordHasher(PasswordEncryptorService passwordEncryptorService) => _passwordEncryptorService = passwordEncryptorService;

    public override string HashPassword(ApplicationUser user, string password) => _passwordEncryptorService.EncryptPassword(password);

    public override PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword) =>
        _passwordEncryptorService.VerifyPassword(providedPassword, hashedPassword)
            ? PasswordVerificationResult.Success
            : PasswordVerificationResult.Failed;
}
using System;
using System.Security.Cryptography;
using FoundersPC.Identity.Application.Interfaces.Services;

namespace FoundersPC.Identity.Services.User_Services
{
    public class PasswordEncryptorService : IPasswordEncryptorService
    {
        public string EncryptPassword(string rawPassword)
        {
            if (ReferenceEquals(rawPassword, null)) throw new ArgumentNullException(nameof(rawPassword));

            var hasher = SHA512.Create();
            var buffer = new byte[64];
            var hash = hasher.ComputeHash(buffer);

            return Convert.ToBase64String(hash);
        }
    }
}

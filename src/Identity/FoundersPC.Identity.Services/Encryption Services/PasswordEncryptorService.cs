using System;
using System.Security.Cryptography;
using System.Text;
using FoundersPC.Identity.Application.Interfaces.Services.Encryption_Services;

namespace FoundersPC.Identity.Services.Encryption_Services
{
    public class PasswordEncryptorService : IPasswordEncryptorService
    {
        public string EncryptPassword(string rawPassword)
        {
            if (ReferenceEquals(rawPassword, null)) throw new ArgumentNullException(nameof(rawPassword));

            var hasher = SHA512.Create();
            var buffer = Encoding.Unicode.GetBytes(rawPassword);
            var hash = hasher.ComputeHash(buffer);

            return Convert.ToBase64String(hash);
        }
    }
}

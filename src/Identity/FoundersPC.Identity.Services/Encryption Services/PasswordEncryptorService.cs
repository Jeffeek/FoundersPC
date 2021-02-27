#region Using namespaces

using System;
using System.Security.Cryptography;
using System.Text;
using FoundersPC.Identity.Application.Interfaces.Services.Encryption_Services;

#endregion

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

        public string GeneratePassword(int length = 6)
        {
            if (length < 6 || length > 30) throw new ArgumentOutOfRangeException(nameof(length));

            var guid = Guid.NewGuid();
            var guidPass = guid.ToString().Replace("-", String.Empty);

            return guidPass.Substring(0, length);
        }
    }
}
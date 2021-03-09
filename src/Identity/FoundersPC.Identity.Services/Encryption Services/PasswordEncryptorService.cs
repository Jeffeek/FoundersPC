#region Using namespaces

using System;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace FoundersPC.Identity.Services.Encryption_Services
{
    public class PasswordEncryptorService
    {
        public string EncryptPassword(string rawPassword)
        {
            if (ReferenceEquals(rawPassword, null)) throw new ArgumentNullException(nameof(rawPassword));

            var passwordBytes = Encoding.ASCII.GetBytes(rawPassword);

            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(passwordBytes);

            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes) hashedInputStringBuilder.Append(b.ToString("X2"));

            return hashedInputStringBuilder.ToString();
        }

        public string GeneratePassword(int length = 6)
        {
            if (length < 6
                || length > 30)
                throw new ArgumentOutOfRangeException(nameof(length));

            var guid = Guid.NewGuid();
            var guidPass = guid.ToString().Replace("-", String.Empty);

            return guidPass.Substring(0, length);
        }
    }
}
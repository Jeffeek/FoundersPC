#region Using namespaces

using System;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace FoundersPC.Identity.Services.Encryption_Services
{
    public class TokenEncryptorService
    {
        public string ComputeHash(string rawToken)
        {
            var tokenBytes = Encoding.Unicode.GetBytes(rawToken);

            using var hash = SHA256.Create();
            var hashedInputBytes = hash.ComputeHash(tokenBytes);

            var hashedInputStringBuilder = new StringBuilder(64);
            foreach (var tokenByte in hashedInputBytes) hashedInputStringBuilder.Append(tokenByte.ToString("X2"));

            return hashedInputStringBuilder.ToString();
        }

        public string CreateRawToken()
        {
            var guid = Guid.NewGuid();

            var rawToken = guid.ToString()
                               .Replace("-", String.Empty)
                               .ToUpperInvariant();

            return rawToken;
        }
    }
}
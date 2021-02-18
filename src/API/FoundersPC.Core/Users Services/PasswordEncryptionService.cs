#region Using namespaces

using System;
using System.Security.Cryptography;
using System.Text;
using FoundersPC.Application.Interfaces.Services.Users.Identity;

#endregion

namespace FoundersPC.Services.Users_Services
{
    // TODO: check impl
    // TODO: change md5 to more secure method of crypt
    public class PasswordEncryptionService : IPasswordEncryptionService
    {
        public string Encrypt(string password)
        {
            if (ReferenceEquals(password, null)) throw new ArgumentNullException(nameof(password));

            using var hash = MD5.Create();
            var hashed = hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            return Convert.ToBase64String(hashed);
        }
    }
}
#region Using namespaces

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

#endregion

namespace FoundersPC.Persistence.Settings
{
    public class EmailBotConfiguration
    {
        #region Docs

        /// <exception cref="T:System.OverflowException">
        ///     <paramref name="s"/> represents a number less than
        ///     <see cref="F:System.Int32.MinValue"/> or greater than <see cref="F:System.Int32.MaxValue"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="s"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.FormatException"><paramref name="s"/> is not in the correct format.</exception>
        /// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Password:Key</exception>

        #endregion

        public EmailBotConfiguration(IConfiguration configuration)
        {
            Host = configuration["Host"];
            Port = Int32.Parse(configuration["Port"]);
            MailAddress = configuration["MailAddress"];

            Password = DecryptPassword(configuration["Password:Key"] ?? throw new KeyNotFoundException("Password:Key"),
                                       configuration["Password:Encrypted"] ?? throw new KeyNotFoundException("Password:Key"),
                                       configuration["Password:IV"] ?? throw new KeyNotFoundException("Password:Key"));
        }

        public string Host { get; }

        public int Port { get; }

        public string MailAddress { get; }

        public string Password { get; }

        private static string DecryptPassword(string key, string password, string iv)
        {
            var buffer = Convert.FromBase64String(password);
            var ivBytes = Encoding.ASCII.GetBytes(iv);
            using var aes = Aes.Create();
            aes.Key = Encoding.ASCII.GetBytes(key);
            aes.IV = ivBytes;
            var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var memoryStream = new MemoryStream(buffer);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);

            return streamReader.ReadToEnd();
        }
    }
}
#region Using namespaces

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

#endregion

namespace FoundersPC.Identity.Domain.Settings
{
	public class EmailBotConfiguration
	{
		public EmailBotConfiguration(IConfiguration configuration)
		{
			Host = configuration["Host"];
			Port = Int32.Parse(configuration["Port"]);
			MailAddress = configuration["MailAddress"];

			Password = DecryptPassword(configuration["Password:Key"],
									   configuration["Password:Encrypted"],
									   configuration["Password:IV"]);
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
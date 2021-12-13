#region Using namespaces

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace FoundersPC.Persistence.Settings;

public class EmailBotConfiguration
{
    public string Host { get; set; }

    public int Port { get; set; }

    public string MailAddress { get; set; }

    public string Password { get; set; }

    public string IV { get; set; }

    public string Key { get; set; }

    public bool Ssl { get; set; }

    public string DecryptPassword()
    {
        var buffer = Convert.FromBase64String(Password);
        var ivBytes = Encoding.ASCII.GetBytes(IV);
        using var aes = Aes.Create();
        aes.Key = Encoding.ASCII.GetBytes(Key);
        aes.IV = ivBytes;
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream(buffer);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);

        return streamReader.ReadToEnd();
    }
}
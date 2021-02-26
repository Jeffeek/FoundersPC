﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Encryption_Services;

namespace FoundersPC.Identity.Services.Encryption_Services
{
    public class TokenEncryptorService : ITokenEncryptorService
    {
        public string Encrypt(string rawToken, string key)
        {
            var keyBytes = Encoding.Unicode.GetBytes(key);
            var tokenBytes = Encoding.Unicode.GetBytes(rawToken);

            using var hasher = new HMACSHA512(keyBytes);
            var hashResult = hasher.ComputeHash(tokenBytes);

            return Convert.ToBase64String(hashResult);
        }

        public string CreateRawToken()
        {
            var guid = Guid.NewGuid();

            var rawToken = guid.ToString().Replace("-", String.Empty).ToUpperInvariant();

            return rawToken;
        }
    }
}

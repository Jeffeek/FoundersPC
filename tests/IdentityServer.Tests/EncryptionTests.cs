#region Using namespaces

using System;
using System.Diagnostics;
using FoundersPC.API.Application.Services;
using NUnit.Framework;

#endregion

namespace IdentityServer.Tests
{
    public class EncryptionTests
    {
        private const int EncryptedPasswordLength = 128;
        private const int MinPasswordLength = 6;
        private const int MaxPasswordLength = 30;

        private const int EncryptedTokenLength = 64;
        private PasswordEncryptorService _passwordEncryptorService;
        private TokenEncryptorService _tokenEncryptorService;

        [OneTimeSetUp]
        public void Setup()
        {
            _passwordEncryptorService = new PasswordEncryptorService();
            _tokenEncryptorService = new TokenEncryptorService();
        }

        [Test]
        public void PasswordEncryption_LengthTest()
        {
            for (var i = 6; i <= 30; i++)
            {
                var randomPassword = _passwordEncryptorService.GeneratePassword(i);
                Trace.WriteLine($"Initial password: {randomPassword}");

                var encryptedPassword = _passwordEncryptorService.EncryptPassword(randomPassword);

                Trace.WriteLine($"Encrypted password: {encryptedPassword}");

                Assert.AreEqual(encryptedPassword.Length, EncryptedPasswordLength);
            }
        }

        [Test]
        public void PasswordEncryption_LengthExceptionTest()
        {
            for (var i = -10; i < MinPasswordLength; i++)
                Assert.Throws<ArgumentOutOfRangeException>(() => _passwordEncryptorService.GeneratePassword(i));

            for (var i = MaxPasswordLength + 1; i < 100; i++)
                Assert.Throws<ArgumentOutOfRangeException>(() => _passwordEncryptorService.GeneratePassword(i));
        }

        [Test]
        public void TokenEncryption_LengthTest()
        {
            for (var i = 0; i < 100; i++)
            {
                var token = _tokenEncryptorService.CreateToken();

                Assert.AreEqual(token.Length, EncryptedTokenLength);
            }
        }
    }
}
#region Using namespaces

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using FoundersPC.Identity.Services.Encryption_Services;

#endregion

namespace IdentityServer.PerformanceTests
{
    [MemoryDiagnoser]
    public class EncryptionPerformanceTests
    {
        private PasswordEncryptorService _passwordEncryptorService;
        private TokenEncryptorService _tokenEncryptorService;

        [ParamsSource(nameof(GetLengths))]
        public int PasswordLength;

        [GlobalSetup]
        public void Setup()
        {
            _passwordEncryptorService = new PasswordEncryptorService();
            _tokenEncryptorService = new TokenEncryptorService();
        }

        [Benchmark]
        public string PasswordEncryption_TimeBenchmark()
        {
            var password = _passwordEncryptorService.GeneratePassword(PasswordLength);

            var encrypted = _passwordEncryptorService.EncryptPassword(password);

            return encrypted;
        }

        [Benchmark]
        public string TokenEncryption_TimeBenchmark()
        {
            var encryptedToken = _tokenEncryptorService.CreateToken();

            return encryptedToken;
        }

        public IEnumerable<int> GetLengths() => Enumerable.Range(6, 24);
    }
}
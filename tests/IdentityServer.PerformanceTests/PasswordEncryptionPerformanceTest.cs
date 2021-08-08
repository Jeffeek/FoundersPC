#region Using namespaces

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FoundersPC.Identity.Services.Encryption_Services;

#endregion

namespace IdentityServer.PerformanceTests
{
    [MemoryDiagnoser]
    [SimpleJob(RuntimeMoniker.NetCoreApp50,
               1,
               1,
               1,
               1)]
    public class PasswordEncryptionPerformanceTest
    {
        private PasswordEncryptorService _passwordEncryptorService;

        [ParamsSource(nameof(GetLengths))]
        public int PasswordLength;

        [GlobalSetup]
        public void Setup() => _passwordEncryptorService = new PasswordEncryptorService();

        [Benchmark]
        public string PasswordEncryption_TimeBenchmark()
        {
            var password = _passwordEncryptorService.GeneratePassword(PasswordLength);

            var encrypted = _passwordEncryptorService.EncryptPassword(password);

            return encrypted;
        }

        public IEnumerable<int> GetLengths() => Enumerable.Range(6, 30);
    }
}
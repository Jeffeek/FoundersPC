#region Using namespaces

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FoundersPC.API.Application.Services;
using FoundersPC.API.Application.Settings;
using Microsoft.Extensions.Options;

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
        public void Setup() => _passwordEncryptorService = new PasswordEncryptorService(Options.Create(new PasswordSettings()
                                                                                                       {
                                                                                                           Salt = "1A133311-1178-203C-M1T6-1234QWERTY99",
                                                                                                           WorkFactor = 12
                                                                                                       }));

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
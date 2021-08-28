#region Using namespaces

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FoundersPC.API.Application.Services;

#endregion

namespace IdentityServer.PerformanceTests
{
    [MemoryDiagnoser]
    [DryJob(RuntimeMoniker.NetCoreApp50)]
    public class TokenCreationPerformanceTest
    {
        private TokenEncryptorService _tokenEncryptorService;

        [GlobalSetup]
        public void Setup() => _tokenEncryptorService = new TokenEncryptorService();

        [Benchmark]
        public string TokenEncryption_TimeBenchmark()
        {
            var encryptedToken = _tokenEncryptorService.CreateToken();

            return encryptedToken;
        }
    }
}
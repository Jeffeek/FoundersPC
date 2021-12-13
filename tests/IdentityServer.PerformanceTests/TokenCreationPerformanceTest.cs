#region Using namespaces

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using FoundersPC.Application.Services;

#endregion

namespace IdentityServer.PerformanceTests;

[MemoryDiagnoser]
[DryJob(RuntimeMoniker.Net60)]
public class TokenCreationPerformanceTest
{
    private TokenEncryptorService _tokenEncryptorService;

    [GlobalSetup]
    public void Setup() => _tokenEncryptorService = new();

    [Benchmark]
    public string TokenEncryption_TimeBenchmark()
    {
        var encryptedToken = _tokenEncryptorService.CreateToken();

        return encryptedToken;
    }
}
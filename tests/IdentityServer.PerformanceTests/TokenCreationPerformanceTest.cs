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
    private AccessTokenFactory _accessTokenFactory;

    [GlobalSetup]
    public void Setup() => _accessTokenFactory = new();

    [Benchmark]
    public string TokenEncryption_TimeBenchmark()
    {
        var encryptedToken = AccessTokenFactory.CreateToken();

        return encryptedToken;
    }
}
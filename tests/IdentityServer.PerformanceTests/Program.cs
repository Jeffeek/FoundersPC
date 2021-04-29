#region Using namespaces

using BenchmarkDotNet.Characteristics;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;

#endregion

namespace IdentityServer.PerformanceTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new DebugBuildConfig();
            BenchmarkRunner.Run<TokenCreationPerformanceTest>(config);
            BenchmarkRunner.Run<PasswordEncryptionPerformanceTest>(config);
        }
    }
}
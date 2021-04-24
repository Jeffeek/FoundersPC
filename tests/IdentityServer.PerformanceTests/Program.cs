#region Using namespaces

using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

#endregion

namespace IdentityServer.PerformanceTests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var config = new DebugBuildConfig();
            BenchmarkRunner.Run<EncryptionPerformanceTests>(config);
        }
    }
}
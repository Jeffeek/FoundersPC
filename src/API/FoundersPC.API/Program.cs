#region Using namespaces

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

#endregion

namespace FoundersPC.API
{
    public class Program
    {
        #region Docs

        /// <exception cref="T:System.IO.IOException">The directory specified by <paramref name="path"/> is read-only.</exception>
        /// <exception cref="T:System.UnauthorizedAccessException">The caller does not have the required permission.</exception>
        /// <exception cref="T:System.IO.DirectoryNotFoundException">The specified path was not found.</exception>
        /// <exception cref="T:System.Security.SecurityException">
        ///     .NET Framework only: The caller does not have the required
        ///     permissions.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">
        ///     <paramref name="path"/> is a zero-length string, contains only white
        ///     space, or contains one or more invalid characters. You can query for invalid characters with the
        ///     <see cref="M:System.IO.Path.GetInvalidPathChars"/> method.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="path"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.IO.PathTooLongException">
        ///     The specified path, file name, or both exceed the system-defined
        ///     maximum length. For more information, see the <see cref="T:System.IO.PathTooLongException"/> topic.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException"><paramref name="path"/> is in an invalid format.</exception>
        /// <exception cref="T:System.InvalidOperationException">When the logger is already created</exception>

        #endregion

        public static async Task Main(string[] args)
        {
            var loggerConfiguration = new ConfigurationBuilder()
                                      .AddJsonFile($"{Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName}\\ApplicationShared\\FoundersPC.ApplicationShared\\LoggerConfiguration.json")
                                      .Build();

            var logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(loggerConfiguration)
                         .CreateLogger();

            Log.Logger = logger;

            try
            {
                Log.Information("HARDWARE API started..");
                var hostBuilder = CreateHostBuilder(args);
                Log.Information("HARDWARE API HostBuilder created");
                var host = hostBuilder.Build();
                Log.Information("Host built");
                await host.RunAsync();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Error when tried to launch the HARDWARE API app");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}
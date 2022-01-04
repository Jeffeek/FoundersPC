#region Using namespaces

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

#endregion

namespace FoundersPC.Web;

public static class Program
{
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
    public static async Task Main(string[] args)
    {
        var loggerConfiguration = new ConfigurationBuilder()
                                  .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "LoggerConfiguration.json"))
                                  .Build();

        var logger = new LoggerConfiguration()
                     .ReadFrom.Configuration(loggerConfiguration)
                     .Enrich.WithThreadId()
                     .Enrich.WithThreadName()
                     .CreateLogger();

        Log.Logger = logger;

        try
        {
            Log.Information("Web server started..");
            var hostBuilder = CreateHostBuilder(args);
            Log.Information("Web server HostBuilder created");
            var host = hostBuilder.Build();
            Log.Information("Host built");
            await host.RunAsync();
        }
        catch (Exception e)
        {
            Log.Fatal(e, "Error when tried to launch the Web server app");
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
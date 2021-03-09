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
		public static async Task Main(string[] args)
		{
			var loggerConfiguration = new ConfigurationBuilder()
									  .SetBasePath(Directory.GetCurrentDirectory())
									  .AddJsonFile("LoggerConfiguration.json")
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
			catch(Exception e)
			{
				Log.Fatal(e, "Error when tried to launch the HARDWARE API app");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
					   .UseSerilog()
					   .ConfigureWebHostDefaults(webBuilder =>
												 {
													 webBuilder
															 .UseStartup<Startup>();
												 });
		}
	}
}
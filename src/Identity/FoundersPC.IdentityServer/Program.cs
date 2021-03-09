#region Using namespaces

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

#endregion

namespace FoundersPC.IdentityServer
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
				Log.Information("Identity server started..");
				var hostBuilder = CreateHostBuilder(args);
				Log.Information("Identity server HostBuilder created");
				var host = hostBuilder.Build();
				Log.Information("Host built");
				await host.RunAsync();
			}
			catch(Exception e)
			{
				Log.Fatal(e, "Error when tried to launch the Identity server app");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
																		   .UseSerilog()
																		   .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
	}
}
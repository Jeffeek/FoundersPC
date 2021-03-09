#region Using namespaces

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

#endregion

namespace FoundersPC.Web
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var loggerConfiguration = new ConfigurationBuilder()
									  .SetBasePath(Directory.GetCurrentDirectory())
									  .AddJsonFile("LoggerConfiguration.json")
									  .Build();

			var logger = new LoggerConfiguration()
						 .ReadFrom.Configuration(loggerConfiguration)
						 .Enrich.WithThreadId()
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
			catch(Exception e)
			{
				Log.Fatal(e, "Error when tried to launch the Web server app");
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
					   .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
		}
	}
}
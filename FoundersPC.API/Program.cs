#region Using derectives

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

#endregion

namespace FoundersPC.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
		                                                                   .ConfigureWebHostDefaults(webBuilder =>
		                                                                   {
			                                                                   webBuilder
				                                                                   .UseStartup<Startup>();
		                                                                   });
	}
}
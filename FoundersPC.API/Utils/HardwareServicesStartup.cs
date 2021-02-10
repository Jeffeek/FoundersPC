using FoundersPC.Core.Hardware_API.Processors;
using FoundersPC.Core.Hardware_API.Producers;
using Microsoft.Extensions.DependencyInjection;

namespace FoundersPC.API.Utils
{
	public static class HardwareServicesStartup
	{
		public static IServiceCollection AddHardwareServices(this IServiceCollection services)
		{
			services.AddScoped<IProducerService, ProducersService>();
			services.AddScoped<ICPUService, CPUService>();
			return services;
		}
	}
}

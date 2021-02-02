using FoundersPC.Application.Interfaces;
using FoundersPC.Services.Hardware_Services;
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

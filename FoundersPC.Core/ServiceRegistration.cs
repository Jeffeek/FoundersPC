#region Using derectives

using FoundersPC.Application.Interfaces.Services;
using FoundersPC.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.Services.Hardware_Services;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Services
{
	public static class ServiceRegistration
	{
		public static void AddHardwareServices(this IServiceCollection services)
		{
			services.AddScoped<IProducerService, ProducersService>();
			services.AddScoped<ICPUService, CPUService>();
			services.AddScoped<IProcessorCoreService, ProcessorCoreService>();
		}
	}
}
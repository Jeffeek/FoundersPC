using FoundersPC.Services.Repositories.CPU;
using FoundersPC.Services.Repositories.ProcessorCores;
using FoundersPC.Services.Repositories.Producer;
using FoundersPC.Services.Repositories.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace FoundersPC.API.Utils
{
	public static class RepositoriesServicesStartup
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<ICPUsRepository, CPUsRepository>();
			services.AddScoped<IProducersRepository, ProducersRepository>();
			services.AddScoped<IProcessorCoresRepository, ProcessorCoresRepository>();
			services.AddScoped<IUnitOfWork, FoundersPCUnitOfWork>();
			return services;
		}
	}
}

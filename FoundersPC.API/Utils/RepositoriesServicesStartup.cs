using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Infrastructure.Repositories;
using FoundersPC.Infrastructure.UoW;
using Microsoft.Extensions.DependencyInjection;

namespace FoundersPC.API.Utils
{
	public static class RepositoriesServicesStartup
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<IProducersRepositoryAsync, ProducersRepository>();
			
			services.AddScoped<ICPUsRepositoryAsync, CPUsRepository>();
			services.AddScoped<IProcessorCoresRepositoryAsync, ProcessorCoresRepository>();

			//TODO: добавить DI для других репозиториев

			services.AddScoped<IUnitOfWorkAsync, FoundersPCUnitOfWork>();
			return services;
		}
	}
}

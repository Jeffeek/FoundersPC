#region Using derectives

using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Application.Interfaces.Repositories.Hardware;
using FoundersPC.Application.Interfaces.Repositories.Hardware.CPU;
using FoundersPC.Application.Interfaces.Repositories.Hardware.GPU;
using FoundersPC.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.Infrastructure.Contexts;
using FoundersPC.Infrastructure.Repositories;
using FoundersPC.Infrastructure.Repositories.Hardware;
using FoundersPC.Infrastructure.Repositories.Hardware.CPU;
using FoundersPC.Infrastructure.Repositories.Hardware.GPU;
using FoundersPC.Infrastructure.Repositories.Hardware.Memory;
using FoundersPC.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Infrastructure
{
	public static class InfrastructureRegistration
	{
		public static void AddRepositories(this IServiceCollection services)
		{
			services.AddScoped<IProducersRepositoryAsync, ProducersRepository>();
			services.AddScoped<ICPUsRepositoryAsync, CPUsRepository>();
			services.AddScoped<IProcessorCoresRepositoryAsync, ProcessorCoresRepository>();
			services.AddScoped<IGPUsRepositoryAsync, GPUsRepository>();
			services.AddScoped<IVideoCardCoresRepositoryAsync, VideoCardCoresRepository>();
			services.AddScoped<ICasesRepositoryAsync, CasesRepository>();
			services.AddScoped<IHDDsRepositoryAsync, HDDsRepository>();
			services.AddScoped<IMotherboardsRepositoryAsync, MotherboardsRepository>();
			services.AddScoped<IPowerSuppliersRepositoryAsync, PowerSuppliersRepository>();
			services.AddScoped<IRAMsRepositoryAsync, RAMsRepository>();
			services.AddScoped<ISSDsRepositoryAsync, SSDsRepository>();
		}

		public static void AddUnitOfWork(this IServiceCollection services)
		{
			services.AddScoped<IUnitOfWorkAsync, FoundersPCUnitOfWork>();
		}

		public static void
			AddFoundersPCHardwareContext(this IServiceCollection services, IConfiguration configuration) =>
			services.AddDbContext<DbContext, FoundersPCDbContext>(options =>
				                                                      options.UseSqlServer(
				                                                       configuration
					                                                       .GetConnectionString("FoundercPC.connectionString"),
				                                                       b =>
					                                                       b.MigrationsAssembly(typeof(
							                                                       FoundersPCDbContext)
						                                                       .Assembly
						                                                       .FullName)));
	}
}
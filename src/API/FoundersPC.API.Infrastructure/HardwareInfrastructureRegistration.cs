#region Using namespaces

using FoundersPC.API.Application.Interfaces.Repositories.Hardware;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.CPU;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.GPU;
using FoundersPC.API.Application.Interfaces.Repositories.Hardware.Memory;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.API.Infrastructure.Repositories;
using FoundersPC.API.Infrastructure.Repositories.Hardware;
using FoundersPC.API.Infrastructure.Repositories.Hardware.CPU;
using FoundersPC.API.Infrastructure.Repositories.Hardware.GPU;
using FoundersPC.API.Infrastructure.Repositories.Hardware.Memory;
using FoundersPC.API.Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.API.Infrastructure
{
    public static class HardwareInfrastructureRegistration
    {
        public static void AddHardwareRepositories(this IServiceCollection services)
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

        public static void AddHardwareUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkHardwareAPI, UnitOfWorkHardwareHardwareAPI>();
        }

        public static void AddFoundersPCHardwareContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FoundersPCHardwareContext>(options =>
                                                                 options.UseSqlServer(configuration
                                                                                          .GetConnectionString("FoundersPC_Hardware"),
                                                                                      b =>
                                                                                          b.MigrationsAssembly(typeof(
                                                                                                  FoundersPCHardwareContext)
                                                                                              .Assembly
                                                                                              .FullName)));
        }
    }
}
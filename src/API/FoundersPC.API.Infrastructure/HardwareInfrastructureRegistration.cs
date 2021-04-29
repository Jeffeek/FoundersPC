#region Using namespaces

using FoundersPC.API.Application.Interfaces.Repositories;
using FoundersPC.API.Application.Interfaces.Repositories.Memory;
using FoundersPC.API.Application.Interfaces.Repositories.Processor;
using FoundersPC.API.Application.Interfaces.Repositories.VideoCard;
using FoundersPC.API.Infrastructure.Contexts;
using FoundersPC.API.Infrastructure.Repositories;
using FoundersPC.API.Infrastructure.Repositories.Hardware;
using FoundersPC.API.Infrastructure.Repositories.Hardware.Memory;
using FoundersPC.API.Infrastructure.Repositories.Hardware.Processor;
using FoundersPC.API.Infrastructure.Repositories.Hardware.VideoCard;
using FoundersPC.API.Infrastructure.UnitOfWork;
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
            services.AddScoped<IProcessorsRepositoryAsync, ProcessorsRepository>();
            services.AddScoped<IProcessorCoresRepositoryAsync, ProcessorCoresRepository>();
            services.AddScoped<IVideoCardsRepositoryAsync, VideoCardsRepository>();
            services.AddScoped<IVideoCardCoresRepositoryAsync, VideoCardCoresRepository>();
            services.AddScoped<ICasesRepositoryAsync, CasesRepository>();
            services.AddScoped<IHardDrivesRepositoryAsync, HardDrivesRepository>();
            services.AddScoped<IMotherboardsRepositoryAsync, MotherboardsRepository>();
            services.AddScoped<IPowerSuppliersRepositoryAsync, PowerSuppliersRepository>();
            services.AddScoped<IRandomAccessMemoryRepositoryAsync, RandomAccessMemoryRepository>();
            services.AddScoped<ISolidStateDrivesRepositoryAsync, SolidStateDrivesRepository>();
        }

        public static void AddHardwareUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWorkHardwareAPI, UnitOfWorkHardwareHardwareAPI>();
        }

        public static void AddFoundersPCHardwareContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbContext, FoundersPCHardwareContext>(options =>
                                                                            options.UseSqlServer(configuration
                                                                                                     .GetConnectionString("FoundersPC_Hardware"),
                                                                                                 b =>
                                                                                                     b.MigrationsAssembly(typeof
                                                                                                                              (
                                                                                                                              FoundersPCHardwareContext
                                                                                                                              )
                                                                                                                          .Assembly
                                                                                                                          .FullName)));
        }
    }
}
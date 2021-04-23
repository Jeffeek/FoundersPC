#region Using namespaces

using FoundersPC.API.Application.Interfaces.Services;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.API.Services.Hardware_Services;
using FoundersPC.API.Services.Hardware_Services.Hardware;
using FoundersPC.API.Services.Hardware_Services.Hardware.Memory;
using FoundersPC.API.Services.Hardware_Services.Hardware.Processor;
using FoundersPC.API.Services.Hardware_Services.Hardware.VideoCard;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.API.Services
{
    public static class HardwareServicesRegistration
    {
        public static void AddHardwareServices(this IServiceCollection services)
        {
            services.AddScoped<IProducersService, ProducersService>();
            services.AddScoped<IProcessorCoresService, ProcessorCoresService>();
            services.AddScoped<IProcessorsService, ProcessorsService>();
            services.AddScoped<IVideoCardCoresService, VideoCardCoresService>();
            services.AddScoped<IVideoCardsService, VideoCardsService>();
            services.AddScoped<IHardDriveDisksService, HardDriveDisksService>();
            services.AddScoped<ISolidStateDrivesService, SolidStateDrivesService>();
            services.AddScoped<IRandomAccessMemoryService, RandomAccessMemoryService>();
            services.AddScoped<ICasesService, CasesService>();
            services.AddScoped<IMotherboardsService, MotherboardsService>();
            services.AddScoped<IPowerSuppliesService, PowerSuppliesService>();
        }
    }
}
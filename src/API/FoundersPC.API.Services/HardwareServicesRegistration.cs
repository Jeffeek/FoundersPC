#region Using namespaces

using FoundersPC.API.Application.Interfaces.Services.Hardware;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.API.Services.Hardware_Services;
using FoundersPC.API.Services.Hardware_Services.Hardware;
using FoundersPC.API.Services.Hardware_Services.Hardware.CPU;
using FoundersPC.API.Services.Hardware_Services.Hardware.GPU;
using FoundersPC.API.Services.Hardware_Services.Hardware.Memory;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

#endregion

namespace FoundersPC.API.Services
{
    public static class HardwareServicesRegistration
    {
        public static void AddHardwareServices(this IServiceCollection services)
        {
            services.AddScoped<IProducerService, ProducersService>();
            services.AddScoped<IProcessorCoreService, ProcessorCoreService>();
            services.AddScoped<ICPUService, CPUService>();
            services.AddScoped<IVideoCardCoreService, VideoCardCoreService>();
            services.AddScoped<IGPUService, GPUService>();
            services.AddScoped<IHDDService, HDDService>();
            services.AddScoped<ISSDService, SSDService>();
            services.AddScoped<IRAMService, RAMService>();
            services.AddScoped<ICaseService, CaseService>();
            services.AddScoped<IMotherboardService, MotherboardService>();
            services.AddScoped<IPowerSupplyService, PowerSupplyService>();
        }
    }
}
#region Using derectives

using FoundersPC.Application.Interfaces.Services;
using FoundersPC.Application.Interfaces.Services.Hardware;
using FoundersPC.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.Services.Hardware_Services;
using FoundersPC.Services.Hardware_Services.Hardware;
using FoundersPC.Services.Hardware_Services.Hardware.CPU;
using FoundersPC.Services.Hardware_Services.Hardware.GPU;
using FoundersPC.Services.Hardware_Services.Hardware.Memory;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Services
{
    public static class ServiceRegistration
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
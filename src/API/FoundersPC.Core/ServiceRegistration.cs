#region Using namespaces

using FoundersPC.Application.Interfaces.Services.Hardware;
using FoundersPC.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.Application.Interfaces.Services.Users;
using FoundersPC.Application.Interfaces.Services.Users.Identity;
using FoundersPC.Services.Hardware_Services;
using FoundersPC.Services.Hardware_Services.Hardware;
using FoundersPC.Services.Hardware_Services.Hardware.CPU;
using FoundersPC.Services.Hardware_Services.Hardware.GPU;
using FoundersPC.Services.Hardware_Services.Hardware.Memory;
using FoundersPC.Services.Users_Services;
using Microsoft.Extensions.Configuration;
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

        public static void AddUserServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailSenderService, EmailSenderService>(provider => new EmailSenderService(configuration));
            services.AddScoped<IPasswordEncryptionService, PasswordEncryptionService>();
            services.AddScoped<IUserIdentityService, UserIdentityService>();
        }
    }
}
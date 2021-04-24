#region Using namespaces

using FluentValidation;
using FluentValidation.AspNetCore;
using FoundersPC.API.Application.HardwareValidation.Hardware.Case;
using FoundersPC.API.Application.Mappings;
using FoundersPC.API.Dto.Mapping;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.API.Application
{
    public static class HardwareApplicationExtensions
    {
        /// <summary>
        ///     Adds mapping settings
        /// </summary>
        /// <param name="services"></param>
        public static void AddHardwareApplicationExtensions(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingStartup));
            services.AddAutoMapper(typeof(HardwareApiDtoMapping));
        }

        /// <summary>
        ///     Adds request validators
        /// </summary>
        /// <param name="services"></param>
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddMvc()
                    .AddFluentValidation(cfg =>
                                         {
                                             cfg.AutomaticValidationEnabled = true;
                                             cfg.RegisterValidatorsFromAssemblyContaining<CaseInsertDtoValidator>();
                                             cfg.ValidatorOptions.CascadeMode = CascadeMode.Stop;
                                         });
        }
    }
}
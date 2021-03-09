using FluentValidation;
using FluentValidation.AspNetCore;
using FoundersPC.Web.Application.Mappings;
using FoundersPC.Web.Application.Validation.AccountSettings;
using Microsoft.Extensions.DependencyInjection;

namespace FoundersPC.Web.Application
{
    public static class WebApplicationExtensions
    {
        public static void AddWebApplicationMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingStartup));
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddMvc()
                    .AddFluentValidation(cfg =>
                                         {
                                             cfg.AutomaticValidationEnabled = true;
                                             cfg.RegisterValidatorsFromAssemblyContaining<PasswordSettingsViewModelValidator>();
                                             cfg.ValidatorOptions.CascadeMode = CascadeMode.Stop;
                                         });
        }
    }
}
#region Using namespaces

using FluentValidation;
using FluentValidation.AspNetCore;
using FoundersPC.Identity.Application.Mappings;
using FoundersPC.Identity.Application.Validation.Requests.Authentication;
using FoundersPC.ServicesShared.Validation;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Identity.Application
{
    public static class IdentityApplicationExtensions
    {
        public static void AddMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingStartup));
        }

        public static void AddValidators(this IServiceCollection services)
        {
            services.AddMvc()
                    .AddFluentValidation(cfg =>
                                         {
                                             cfg.AutomaticValidationEnabled = true;
                                             cfg.RegisterValidatorsFromAssemblyContaining<UserForgotPasswordRequestValidator>();
                                             cfg.RegisterValidatorsFromAssembly(typeof(PaginationRequestValidator).Assembly);
                                             cfg.ValidatorOptions.CascadeMode = CascadeMode.Stop;
                                         });
        }
    }
}
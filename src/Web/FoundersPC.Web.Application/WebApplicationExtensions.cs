#region Using namespaces

using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using FoundersPC.API.Dto.Mapping;
using FoundersPC.Web.Application.Mappings;
using FoundersPC.Web.Application.Validation.AccountSettings;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Web.Application
{
    public static class WebApplicationExtensions
    {
        public static void AddWebApplicationMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingStartup));
            services.AddAutoMapper(typeof(HardwareApiDtoMapping));
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

        public static void AddCookieSecureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                               {
                                   options.Cookie = new CookieBuilder
                                                    {
                                                        HttpOnly = true,
                                                        IsEssential = true,
                                                        Name = "user_cred",
                                                        Path = "/",
                                                        SecurePolicy = CookieSecurePolicy.SameAsRequest,
                                                        SameSite = SameSiteMode.Strict
                                                    };

                                   options.LoginPath = "/Authentication/SignIn";
                                   options.AccessDeniedPath = "/Error/403";
                                   options.LogoutPath = "/Authentication/SignOut";

                                   options.ExpireTimeSpan = TimeSpan.FromDays(30);
                               });
        }

        public static string GetJwtTokenFromCookie(this HttpContext context)
        {
            context.Request.Cookies.TryGetValue("token", out var token);

            return token;
        }
    }
}
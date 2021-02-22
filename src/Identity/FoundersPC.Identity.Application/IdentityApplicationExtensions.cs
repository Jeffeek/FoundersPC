#region Using namespaces

using FoundersPC.Identity.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Identity.Application
{
    public static class IdentityApplicationExtensions
    {
        public static void AddUserApplicationExtensions(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingStartup));
        }
    }
}
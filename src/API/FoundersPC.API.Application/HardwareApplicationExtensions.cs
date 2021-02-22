#region Using namespaces

using FoundersPC.API.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.API.Application
{
    public static class HardwareApplicationExtensions
    {
        public static void AddHardwareApplicationExtensions(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingStartup));
        }
    }
}
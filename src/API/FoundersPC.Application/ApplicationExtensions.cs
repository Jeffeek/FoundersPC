#region Using derectives

using FoundersPC.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

#endregion

namespace FoundersPC.Application
{
	public static class ApplicationExtensions
	{
		public static void AddApplicationExtensions(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(MappingStartup));
		}
	}
}
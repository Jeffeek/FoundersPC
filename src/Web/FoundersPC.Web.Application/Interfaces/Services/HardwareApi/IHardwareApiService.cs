#region Using namespaces

using System.Threading.Tasks;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
	public interface IHardwareApiService
	{
		Task<string> GetStringAsync(string entityType, string token);
	}
}
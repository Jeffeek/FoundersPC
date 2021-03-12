#region Using namespaces

using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

#endregion

namespace FoundersPC.Web.Services.Web_Services
{
	public class MicroservicesBaseAddresses
	{
		public MicroservicesBaseAddresses(IConfiguration configuration)
		{
			HardwareApiBaseAddress = configuration["ConnectionServers:API"] ?? throw new KeyNotFoundException(nameof(configuration));
			IdentityApiBaseAddress = configuration["ConnectionServers:IdentityServer"] ?? throw new KeyNotFoundException(nameof(configuration));
		}

		public string HardwareApiBaseAddress { get; }

		public string IdentityApiBaseAddress { get; }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FoundersPC.Web.Services.Web_Services
{
    public class MicroservicesBaseAddresses
    {
        public string HardwareApiBaseAddress { get; }

        public string IdentityApiBaseAddress { get; }

        public MicroservicesBaseAddresses(IConfiguration configuration)
        {
            HardwareApiBaseAddress = configuration["ConnectionServers:API"] ?? throw new KeyNotFoundException(nameof(configuration));
            IdentityApiBaseAddress = configuration["ConnectionServers:IdentityServer"] ?? throw new KeyNotFoundException(nameof(configuration));
        }
    }
}

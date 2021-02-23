#region Using namespaces

#endregion

using Microsoft.Extensions.Configuration;

namespace FoundersPC.Identity.Domain.Settings
{
    public class TokenConfiguration
    {
        public string HashTokenKey { get; }

        public TokenConfiguration(IConfiguration configuration)
        {
            HashTokenKey = configuration["TokenConfiguration:HashTokenKey"];
        }
    }
}
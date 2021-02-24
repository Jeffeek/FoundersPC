#region Using namespaces

using Microsoft.Extensions.Configuration;

#endregion

namespace FoundersPC.Identity.Domain.Settings
{
    public class TokenConfiguration
    {
        public TokenConfiguration(IConfiguration configuration) => HashTokenKey = configuration["TokenConfiguration:HashTokenKey"];

        public string HashTokenKey { get; }
    }
}
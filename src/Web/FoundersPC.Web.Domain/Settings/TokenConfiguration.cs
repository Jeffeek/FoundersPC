#region Using namespaces

using Microsoft.Extensions.Configuration;

#endregion

namespace FoundersPC.Web.Domain.Settings
{
    public class TokenConfiguration
    {
        public string HashTokenKey;

        public TokenConfiguration(IConfiguration configuration) => HashTokenKey = configuration["TokenConfiguration:HashTokenKey"];
    }
}
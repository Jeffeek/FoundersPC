#region Using namespaces

#endregion

using Microsoft.Extensions.Configuration;

namespace FoundersPC.Web.Domain.Settings
{
    public class TokenConfiguration
    {
        public string HashTokenKey;

        public TokenConfiguration(IConfiguration configuration)
        {
            HashTokenKey = configuration["TokenConfiguration:HashTokenKey"];
        }
    }
}
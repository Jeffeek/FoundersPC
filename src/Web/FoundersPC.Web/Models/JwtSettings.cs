#region Using namespaces

using Microsoft.Extensions.Configuration;

#endregion

namespace FoundersPC.Web.Models
{
    public static class JwtSettings
    {
        private static bool _isConfigured;
        public static string Issuer;
        public static string Audience;
        public static string SecretKey;

        public static void Initialize(IConfiguration configuration)
        {
            if (_isConfigured) return;

            Issuer = configuration["Jwt:Issuer"];
            Audience = configuration["Jwt:Audience"];
            SecretKey = configuration["Jwt:SecretKey"];
            _isConfigured = true;
        }
    }
}
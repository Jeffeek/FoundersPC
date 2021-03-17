#region Using namespaces

using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.ApplicationShared
{
    // todo: make this class as singleton service, take credentials behind config / file
    public class JwtConfiguration
    {
        public JwtConfiguration(IConfiguration configuration)
        {
            Issuer = configuration["JwtSettings:Issuer"];
            Audience = configuration["JwtSettings:Audience"];
            Key = configuration["JwtSettings:Key"];
        }

        public string Issuer { get; }

        public string Audience { get; }

        private string Key { get; }

        public SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.ASCII.GetBytes(Key));
    }
}
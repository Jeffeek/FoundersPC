#region Using namespaces

using System;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.ApplicationShared.Jwt
{
    public class JwtConfiguration
    {
        private static JwtConfiguration _configuration;

        private JwtConfiguration() { }

        public static JwtConfiguration Configuration
        {
            get
            {
                if (_configuration is null) throw new NotSupportedException();

                return _configuration;
            }

            private set
            {
                if (_configuration is not null) return;

                _configuration = value;
            }
        }

        public string Key { get; private init; }

        public string Issuer { get; private init; }

        public string Audience { get; private init; }

        public int HoursToExpire { get; private init; }

        public static void Initialize(IConfiguration configuration)
        {
            Configuration = new JwtConfiguration
                            {
                                Issuer = configuration["JwtSettings:Issuer"],
                                Audience = configuration["JwtSettings:Audience"],
                                Key = configuration["JwtSettings:Key"],
                                HoursToExpire = Int32.Parse(configuration["JwtSettings:HoursToExpire"])
                            };
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.ASCII.GetBytes(Key));
    }
}
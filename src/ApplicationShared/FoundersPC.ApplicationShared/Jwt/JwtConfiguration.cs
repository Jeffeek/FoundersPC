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

        /// <exception cref="T:System.NotSupportedException" accessor="get">Inner JWT configuration was null.</exception>
        internal static JwtConfiguration Configuration
        {
            get
            {
                if (_configuration is null)
                    throw new NotSupportedException();

                return _configuration;
            }

            private set
            {
                if (_configuration is not null)
                    return;

                _configuration = value;
            }
        }

        public string Key { get; private init; }

        public string Issuer { get; private init; }

        public string Audience { get; private init; }

        public int HoursToExpire { get; private init; }

        /// <exception cref="T:System.OverflowException"><paramref name="configuration[JwtSettings:HoursToExpire]" /> represents a number less than <see cref="F:System.Int32.MinValue" /> or greater than <see cref="F:System.Int32.MaxValue" />.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="configuration[JwtSettings:HoursToExpire]" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.FormatException"><paramref name="configuration[JwtSettings:HoursToExpire]" /> is not in the correct format.</exception>
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

        /// <exception cref="T:System.ArgumentNullException"><paramref name="Key" /> is <see langword="null" />.</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">A fallback occurred (for more information, see Character Encoding in .NET)
        ///  -and-
        ///  <see cref="P:System.Text.Encoding.EncoderFallback" /> is set to <see cref="T:System.Text.EncoderExceptionFallback" />.</exception>
        internal SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.ASCII.GetBytes(Key));
    }
}
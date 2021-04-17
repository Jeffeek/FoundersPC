#region Using namespaces

using System;
using System.Data;
using System.Linq;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.ApplicationShared
{
    public static class ApplicationSharedExtensions
    {
        #region Docs

        /// <exception cref="T:System.OverflowException">
        ///     <paramref name="configuration[JwtSettings:HoursToExpire]"/> represents a
        ///     number less than <see cref="F:System.Int32.MinValue"/> or greater than <see cref="F:System.Int32.MaxValue"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="configuration[JwtSettings:HoursToExpire]"/> is
        ///     <see langword="null"/>.
        /// </exception>
        /// <exception cref="T:System.NotSupportedException">Inner JWT configuration was null.</exception>
        /// <exception cref="T:System.FormatException">
        ///     <paramref name="configuration[JwtSettings:HoursToExpire]"/> is not in the
        ///     correct format.
        /// </exception>

        #endregion

        public static void AddJwtSettings(this IServiceCollection services, IConfiguration configuration)
        {
            JwtConfiguration.Initialize(configuration);
            services.AddSingleton(JwtConfiguration.Configuration);
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="predicate"/> is
        ///     <see langword="null"/>.
        /// </exception>
        /// <exception cref="T:System.Data.NoNullAllowedException">Condition.</exception>
        /// <exception cref="T:System.TypeInitializationException">Bearer settings middleware not found</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">
        ///     A fallback occurred (for more information, see Character Encoding in .NET)
        ///     -and-
        ///     <see cref="P:System.Text.Encoding.EncoderFallback"/> is set to <see cref="T:System.Text.EncoderExceptionFallback"/>
        ///     .
        /// </exception>
        /// <exception cref="T:System.OverflowException">
        ///     <paramref name="value"/> is less than <see cref="F:System.TimeSpan.MinValue"/> or greater than
        ///     <see cref="F:System.TimeSpan.MaxValue"/>.
        ///     -or-
        ///     <paramref name="value"/> is <see cref="F:System.Double.PositiveInfinity"/>.
        ///     -or-
        ///     <paramref name="value"/> is <see cref="F:System.Double.NegativeInfinity"/>.
        /// </exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="value"/> is equal to <see cref="F:System.Double.NaN"/>.</exception>

        #endregion

        public static void AddBearerAuthenticationWithSettings(this IServiceCollection services)
        {
            var jwtConfigService = services.FirstOrDefault(x => x.ServiceType == typeof(JwtConfiguration));

            if (jwtConfigService is null)
                throw new NoNullAllowedException(nameof(jwtConfigService));

            if (jwtConfigService.ImplementationInstance is not JwtConfiguration service)
                throw new TypeInitializationException("Bearer settings middleware not found", null);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
                                  config =>
                                  {
                                      var key = service.GetSymmetricSecurityKey();
                                      config.BackchannelTimeout = TimeSpan.FromSeconds(20);

                                      config.TokenValidationParameters = new TokenValidationParameters
                                                                         {
                                                                             ValidateAudience = false,
                                                                             ValidIssuer = service.Issuer,
                                                                             ValidAudience = service.Audience,
                                                                             IssuerSigningKey = key
                                                                         };
                                  });
        }

        public static void AddAuthorizationPolicies(this IServiceCollection services, string scheme)
        {
            services.AddAuthorization(configuration =>
                                      {
                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.AdministratorPolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                                    .RequireAuthenticatedUser()
                                                                                    .RequireRole(ApplicationRoles
                                                                                                     .Administrator)
                                                                                    .Build());

                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.ManagerPolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                                    .RequireAuthenticatedUser()
                                                                                    .RequireRole(ApplicationRoles
                                                                                                     .Manager)
                                                                                    .Build());

                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.DefaultUserPolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                                    .RequireAuthenticatedUser()
                                                                                    .RequireRole(ApplicationRoles
                                                                                                     .DefaultUser)
                                                                                    .Build());

                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.EmployeePolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                                    .RequireAuthenticatedUser()
                                                                                    .RequireRole(ApplicationRoles.Administrator,
                                                                                                 ApplicationRoles.Manager)
                                                                                    .Build());

                                          configuration.AddPolicy(ApplicationAuthorizationPolicies.AuthenticatedPolicy,
                                                                  builder => builder.AddAuthenticationSchemes(scheme)
                                                                                    .RequireAuthenticatedUser()
                                                                                    .Build());
                                      });
        }
    }
}
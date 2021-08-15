#region Using namespaces

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.SharedKernel.Jwt
{
    public class JwtUserToken
    {
        private readonly JwtConfiguration _configuration;

        public JwtUserToken(JwtConfiguration configuration) => _configuration = configuration;

        public string Email { get; init; }

        public string Role { get; init; }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="Key"/> is <see langword="null"/>.</exception>
        /// <exception cref="T:System.Text.EncoderFallbackException">
        ///     A fallback occurred (for more information, see Character Encoding in .NET)
        ///     -and-
        ///     <see cref="P:System.Text.Encoding.EncoderFallback"/> is set to <see cref="T:System.Text.EncoderExceptionFallback"/>
        ///     .
        /// </exception>
        /// <exception cref="T:System.ArgumentException">If 'expires' &lt;= 'notbefore'.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     The resulting <see cref="T:System.DateTime"/> is less than
        ///     <see cref="F:System.DateTime.MinValue"/> or greater than <see cref="F:System.DateTime.MaxValue"/>.
        /// </exception>
        /// <exception cref="T:Microsoft.IdentityModel.Tokens.SecurityTokenEncryptionFailedException">
        ///     both
        ///     <see cref="P:System.IdentityModel.Tokens.Jwt.JwtSecurityToken.SigningCredentials"/> and
        ///     <see cref="P:System.IdentityModel.Tokens.Jwt.JwtSecurityToken.InnerToken"/> are set.
        /// </exception>

        #endregion

        public string GetToken()
        {
            var claims = new List<Claim>
                         {
                             new(ClaimsIdentity.DefaultRoleClaimType, Role),
                             new(ClaimsIdentity.DefaultNameClaimType, Email)
                         };

            var key = _configuration.GetSymmetricSecurityKey();

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration.Issuer,
                                             _configuration.Audience,
                                             claims,
                                             DateTime.Now,
                                             DateTime.Now.AddMinutes(60),
                                             signingCredentials);

            var value = new JwtSecurityTokenHandler().WriteToken(token);

            return value;
        }
    }
}
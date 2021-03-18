#region Using namespaces

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.ApplicationShared
{
    public class JwtUserToken
    {
        private readonly JwtConfiguration _configuration;

        public JwtUserToken(JwtConfiguration configuration) => _configuration = configuration;

        public string Email { get; init; }

        public string Role { get; init; }

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
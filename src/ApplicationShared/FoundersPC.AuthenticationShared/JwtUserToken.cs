#region Using namespaces

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.AuthenticationShared
{
    public class JwtUserToken
    {
        public JwtUserToken(string email, string role, int userId)
        {
            Email = email;
            Role = role;
            UserId = userId;
        }

        public int UserId { get; set; }

        public string Email { get; }

        public string Role { get; }

        public string GetToken()
        {
            var claims = new List<Claim>
                         {
                             new(ClaimsIdentity.DefaultRoleClaimType, Role),
                             new(ClaimsIdentity.DefaultNameClaimType, Email),
                             new(JwtRegisteredClaimNames.NameId, UserId.ToString())
                         };

            var key = JwtConfiguration.GetSymmetricSecurityKey();

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(JwtConfiguration.Issuer,
                                             JwtConfiguration.Audience,
                                             claims,
                                             DateTime.Now,
                                             DateTime.Now.AddMinutes(60),
                                             signingCredentials);

            var value = new JwtSecurityTokenHandler().WriteToken(token);

            return value;
        }
    }
}
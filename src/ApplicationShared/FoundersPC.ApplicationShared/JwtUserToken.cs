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
		public JwtUserToken(string email, string role)
		{
			Email = email;
			Role = role;
		}

		public string Email { get; }

		public string Role { get; }

		public string GetToken()
		{
			var claims = new List<Claim>
						 {
								 new(ClaimsIdentity.DefaultRoleClaimType, Role),
								 new(ClaimsIdentity.DefaultNameClaimType, Email)
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
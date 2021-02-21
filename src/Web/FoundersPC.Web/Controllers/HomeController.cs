#region Using namespaces

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FoundersPC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

#endregion

namespace FoundersPC.Web.Controllers
{
    public sealed class HomeController : Controller
    {
        public IActionResult Index() => View();

        public IActionResult Authenticate()
        {
            // find user

            var claims = new List<Claim>
                         {
                             new(JwtRegisteredClaimNames.Sub, "Arcadiy"),
                             new(JwtRegisteredClaimNames.Email, "arc@mail.com")
                         };

            var secretBytes = Encoding.UTF8.GetBytes(JwtSettings.SecretKey);

            var key = new SymmetricSecurityKey(secretBytes);

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(JwtSettings.Issuer,
                                             JwtSettings.Audience,
                                             claims,
                                             DateTime.Now,
                                             DateTime.Now.AddMinutes(60),
                                             signingCredentials);

            var value = new JwtSecurityTokenHandler().WriteToken(token);

            ViewBag.Token = value;

            return View();
        }
    }
}
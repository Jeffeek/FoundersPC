#region Using namespaces

using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.RequestResponseShared.Request.Tokens;
using FoundersPC.Web.Application.Interfaces.Services.Pricing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [AllowAnonymous]
    [Route("Pricing")]
    public class PricingController : Controller
    {
        private readonly ITokenReservationWebService _tokenReservationService;

        public PricingController(ITokenReservationWebService tokenReservationService) => _tokenReservationService = tokenReservationService;

        [Authorize(Policy = ApplicationAuthorizationPolicies.DefaultUserPolicy)]
        [Route("Buy/{tokenType}")]
        public async Task<IActionResult> Buy([FromRoute] string tokenType)
        {
            if (tokenType is null) return BadRequest();

            var isValidTokenType = Enum.GetNames<TokenType>().Contains(tokenType);

            if (!isValidTokenType) BadRequest();

            var userEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

            HttpContext.Request.Cookies.TryGetValue("token", out var jwtToken);

            if (userEmail is null) return RedirectToAction("LogOut", "Authentication");

            if (jwtToken is null) throw new CookieException();

            var result =
                await _tokenReservationService.ReserveNewTokenAsync(Enum.Parse<TokenType>(tokenType),
                                                                    userEmail,
                                                                    jwtToken);

            if (result is null || !result.IsBuyingSuccessful) return RedirectToAction("ServerErrorIndex", "Error");

            return RedirectToAction("Profile", "Account");
        }
    }
}
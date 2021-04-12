#region Using namespaces

using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Request.Tokens;
using FoundersPC.Web.Application;
using FoundersPC.Web.Application.Interfaces.Services.Pricing;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        public PricingController(ITokenReservationWebService tokenReservationService) =>
            _tokenReservationService = tokenReservationService;

        [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy,
                   AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        [Route("Buy/{tokenType}")]
        public async Task<IActionResult> Buy([FromRoute] string tokenType)
        {
            if (tokenType is null)
                return BadRequest();

            var isValidTokenType = Enum.GetNames<TokenType>()
                                       .Contains(tokenType);

            if (!isValidTokenType)
                BadRequest();

            var userEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

            if (userEmail is null)
                return RedirectToAction("LogOut", "Authentication");

            var result =
                await _tokenReservationService.ReserveNewTokenAsync(Enum.Parse<TokenType>(tokenType),
                                                                    userEmail,
                                                                    HttpContext.GetJwtTokenFromCookie());

            if (result is null
                || !result.IsBuyingSuccessful)
                return RedirectToAction("ServerErrorIndex", "Error");

            return RedirectToAction("Profile", "Account");
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.RequestResponseShared.Request.Tokens;
using FoundersPC.Web.Application.Interfaces.Services.Pricing;

namespace FoundersPC.Web.Controllers
{
    // todo: make service
    public class PricingController : Controller
    {
        private readonly ITokenReservationWebService _tokenReservationService;

        public PricingController(ITokenReservationWebService tokenReservationService)
        {
            _tokenReservationService = tokenReservationService;
        }

        [HttpGet]
        public IActionResult Pricing() => View();

        [Route("Buy/{tokenType}")]
        public async Task<IActionResult> Buy(string tokenType)
        {
            if (tokenType is null) return BadRequest();

            var isValid = Enum.GetNames<TokenType>().Contains(tokenType);

            if (!isValid) BadRequest();

            var userEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);

            HttpContext.Request.Cookies.TryGetValue("token", out var jwtToken);

            if (userEmail is null)
                return RedirectToAction("LogOut", "Authentication");

            var result =
                await _tokenReservationService.ReserveNewTokenAsync(Enum.Parse<TokenType>(tokenType),
                                                                    userEmail,
                                                                    jwtToken);

            if (result is null || !result.IsBuyingSuccessful) return RedirectToAction("ServerErrorIndex", "Error");

            return Json(result.Token);
        }
    }
}

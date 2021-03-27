#region Using namespaces

using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.RequestResponseShared.Request.Tokens;
using FoundersPC.RequestResponseShared.Response.Tokens;
using FoundersPC.WebIdentityShared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Tokens
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("FoundersPCIdentity/Tokens")]
    [ApiController]
    public class TokensWebController : Controller
    {
        private readonly IApiAccessUsersTokensService _apiAccessUsersTokensService;
        private readonly IApiAccessTokensReservationService _accessTokensReservationService;

        public TokensWebController(IApiAccessUsersTokensService apiAccessUsersTokensService,
                                   IApiAccessTokensReservationService accessTokensReservationService)
        {
            _apiAccessUsersTokensService = apiAccessUsersTokensService;
            _accessTokensReservationService = accessTokensReservationService;
        }

        [HttpGet]
        [Route("User/{email}")]
        public async Task<ActionResult<IEnumerable<ApplicationAccessToken>>> GetUserTokens(string email)
        {
            var tokens = await _apiAccessUsersTokensService.GetUserTokens(email);

            if (tokens is null)
                return BadRequest(new
                                  {
                                      error = "No user with this email"
                                  });

            return Json(tokens);
        }

        // todo: validator for request
        [HttpPost]
        [Route("Reserve")]
        public async Task<ActionResult<BuyNewTokenResponse>> ReserveNewToken([FromBody] BuyNewTokenRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var newTokenResult =
                await _accessTokensReservationService.ReserveNewTokenAsync(request.UserEmail, request.TokenType);

            if (newTokenResult is null)
                return new BuyNewTokenResponse()
                       {
                           IsBuyingSuccessful = true,
                           Token = null
                       };

            return new BuyNewTokenResponse()
                   {
                       IsBuyingSuccessful = true,
                       Token = newTokenResult
                   };
        }
    }
}
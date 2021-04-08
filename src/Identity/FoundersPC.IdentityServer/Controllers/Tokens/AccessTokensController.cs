#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Request.Tokens;
using FoundersPC.RequestResponseShared.Response.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Tokens
{
    [EnableCors("WebPolicy")]
    [Route("FoundersPCIdentity/Tokens")]
    [ApiController]
    public class AccessTokensController : Controller
    {
        private readonly IApiAccessTokensReservationService _accessTokensReservationService;
        private readonly IApiAccessUsersTokensService _apiAccessUsersTokensService;

        public AccessTokensController(IApiAccessUsersTokensService apiAccessUsersTokensService,
                                      IApiAccessTokensReservationService accessTokensReservationService)
        {
            _apiAccessUsersTokensService = apiAccessUsersTokensService;
            _accessTokensReservationService = accessTokensReservationService;
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet("User/{email}")]
        public async Task<ActionResult<IEnumerable<ApiAccessUserTokenReadDto>>> GetUserTokens([FromRoute] string email)
        {
            var tokens = await _apiAccessUsersTokensService.GetUserTokens(email);

            if (tokens is null)
                return BadRequest(new
                                  {
                                      error = "No user with this email"
                                  });

            return Json(tokens);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
        [HttpPost("Reserve")]
        public async Task<ActionResult<BuyNewTokenResponse>> ReserveNewToken([FromBody] BuyNewTokenRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var newTokenResult =
                await _accessTokensReservationService.ReserveNewTokenAsync(request.UserEmail, request.TokenType);

            if (newTokenResult is null)
                return new BuyNewTokenResponse
                       {
                           IsBuyingSuccessful = true,
                           Token = null
                       };

            return new BuyNewTokenResponse
                   {
                       IsBuyingSuccessful = true,
                       Token = newTokenResult
                   };
        }

        [EnableCors("TokenCheckPolicy")]
        [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
        [HttpGet("Check/{token:length(64)}")]
        public async Task<ActionResult> CheckTokenForUsability([FromRoute] string token)
        {
            if (token is null || token.Length != 64) return BadRequest();

            var checkTokenResult = await _apiAccessUsersTokensService.CanMakeRequestAsync(token);

            if (!checkTokenResult) return Forbid();

            return Ok();
        }
    }
}
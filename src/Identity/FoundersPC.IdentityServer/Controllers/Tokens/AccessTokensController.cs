#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
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
    [Route("FoundersPCIdentity/Tokens")]
    [ApiController]
    [EnableCors(ApplicationCorsPolicies.WebClientPolicy)]
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
            var tokens = await _apiAccessUsersTokensService.GetUserTokensAsync(email);

            if (tokens is null)
                return BadRequest(new
                                  {
                                      error = "No user with this email"
                                  });

            return Json(tokens);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet("User/{id:int:min(1)}")]
        public async Task<ActionResult<IEnumerable<ApiAccessUserTokenReadDto>>> GetUserTokens([FromRoute] int id)
        {
            var tokens = await _apiAccessUsersTokensService.GetUserTokensAsync(id);

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
            if (!ModelState.IsValid)
                return BadRequest();

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

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpPut("Block/ById/{tokenId:int:min(1)}")]
        public async Task<ActionResult> BlockTokenByTokenId([FromRoute] int tokenId)
        {
            var tokenBlockingResult = await _apiAccessUsersTokensService.BlockAsync(tokenId);

            if (tokenBlockingResult)
                return Ok();

            return Problem();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpPut("Block/ByToken/{token:length(64)}")]
        public async Task<ActionResult> BlockTokenByTokenString([FromRoute] int token)
        {
            var tokenBlockingResult = await _apiAccessUsersTokensService.BlockAsync(token);

            if (tokenBlockingResult)
                return Ok();

            return Problem();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet]
        public async Task<IEnumerable<ApiAccessUserTokenReadDto>> GetPaginateableTokens([FromQuery(Name = "Page")] int pageNumber,
                                                                                        [FromQuery(Name = "Size")] int pageSize = FoundersPCConstants.PageSize) =>
            await _apiAccessUsersTokensService.GetPaginateableAsync(pageNumber, pageSize);

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet("All")]
        public async Task<IEnumerable<ApiAccessUserTokenReadDto>> GetAll() =>
            await _apiAccessUsersTokensService.GetAllTokensAsync();

        [EnableCors(ApplicationCorsPolicies.TokenCheckPolicy)]
        [AllowAnonymous]
        [HttpGet("Check/{token:length(64)}")]
        public async Task<ActionResult> CheckTokenForUsability([FromRoute] string token,
                                                               [FromServices] IAccessTokensLogsService logsService)
        {
            if (token is null
                || token.Length != 64)
                return BadRequest();

            var checkTokenResult = await _apiAccessUsersTokensService.CanMakeRequestAsync(token);

            if (!checkTokenResult)
                return Forbid();

            await logsService.LogAsync(token);

            return Ok();
        }
    }
}
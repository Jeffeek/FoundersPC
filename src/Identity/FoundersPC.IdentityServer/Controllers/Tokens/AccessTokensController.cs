#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.ApplicationShared.Middleware;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Tokens;
using FoundersPC.RequestResponseShared.IdentityServer.Response.Tokens;
using FoundersPC.RequestResponseShared.Pagination.Requests;
using FoundersPC.RequestResponseShared.Pagination.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Tokens
{
    [ApiController]
    [EnableCors(ApplicationCorsPolicies.WebClientPolicy)]
    [Route(IdentityServerRoutes.Tokens.TokensEndpoint)]
    [ModelValidation]
    public class AccessTokensController : Controller
    {
        private readonly IAccessTokensReservationService _accessTokensReservationService;
        private readonly IAccessUsersTokensService _accessUsersTokensService;

        public AccessTokensController(IAccessUsersTokensService accessUsersTokensService,
                                      IAccessTokensReservationService accessTokensReservationService)
        {
            _accessUsersTokensService = accessUsersTokensService;
            _accessTokensReservationService = accessTokensReservationService;
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet(IdentityServerRoutes.Tokens.ByUserEmail)]
        public async ValueTask<ActionResult<IEnumerable<AccessUserTokenReadDto>>> GetUserTokens([FromRoute] string email)
        {
            var tokens = await _accessUsersTokensService.GetUserTokensAsync(email);

            if (tokens is null)
                return BadRequest(new
                                  {
                                      error = "No user with this email"
                                  });

            return Json(tokens);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet(IdentityServerRoutes.Tokens.ByUserId)]
        public async ValueTask<ActionResult<IEnumerable<AccessUserTokenReadDto>>> GetUserTokens([FromRoute] int id)
        {
            var tokens = await _accessUsersTokensService.GetUserTokensAsync(id);

            if (tokens is null)
                return BadRequest(new
                                  {
                                      error = "No user with this email"
                                  });

            return Json(tokens);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
        [HttpPost(IdentityServerRoutes.Tokens.ReserveNewToken)]
        public async ValueTask<ActionResult<BuyNewTokenResponse>> ReserveNewToken([FromBody] BuyNewTokenRequest request)
        {
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
        [HttpPut(IdentityServerRoutes.Tokens.BlockByTokenId)]
        public async ValueTask<ActionResult> BlockTokenByTokenId([FromRoute] int id)
        {
            var tokenBlockingResult = await _accessUsersTokensService.BlockAsync(id);

            if (tokenBlockingResult)
                return Ok();

            return Problem();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpPut(IdentityServerRoutes.Tokens.BlockByTokenString)]
        public async ValueTask<ActionResult> BlockTokenByTokenString([FromRoute] int token)
        {
            var tokenBlockingResult = await _accessUsersTokensService.BlockAsync(token);

            if (tokenBlockingResult)
                return Ok();

            return Problem();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet]
        public async ValueTask<IPaginationResponse<AccessUserTokenReadDto>> GetPaginateableTokens([FromQuery] PaginationRequest request) =>
            await _accessUsersTokensService.GetPaginateableAsync(request.PageNumber, request.PageSize);

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet(ApplicationRestAddons.All)]
        public async ValueTask<IEnumerable<AccessUserTokenReadDto>> GetAll() => await _accessUsersTokensService.GetAllTokensAsync();

        [EnableCors(ApplicationCorsPolicies.TokenCheckPolicy)]
        [AllowAnonymous]
        [HttpGet(IdentityServerRoutes.Tokens.CheckToken)]
        public async ValueTask<ActionResult> CheckTokenForUsability([FromRoute] string token,
                                                                    [FromServices] IAccessTokensLogsService logsService)
        {
            if (token is null
                || token.Length != 64)
                return BadRequest();

            var checkTokenResult = await _accessUsersTokensService.CanMakeRequestAsync(token);

            if (!checkTokenResult)
                return Forbid();

            await logsService.LogAsync(token);

            return Ok();
        }
    }
}
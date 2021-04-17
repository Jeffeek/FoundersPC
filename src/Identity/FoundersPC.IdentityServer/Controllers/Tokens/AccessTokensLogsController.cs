#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Pagination;
using FoundersPC.RequestResponseShared.Pagination.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Tokens
{
    [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
    [ApiController]
    [Route(IdentityServerRoutes.Logs.TokenUsages.TokenUsagesEndpoint)]
    public class AccessTokensLogsController : Controller
    {
        private readonly IAccessTokensLogsService _accessTokensLogsService;

        public AccessTokensLogsController(IAccessTokensLogsService accessTokensLogsService) => _accessTokensLogsService = accessTokensLogsService;

        [HttpGet(ApplicationRestAddons.All)]
        public async ValueTask<IEnumerable<AccessTokenLogReadDto>> GetAll() =>
            await _accessTokensLogsService.GetAllTokensLogsAsync();

        [HttpGet]
        public async ValueTask<ActionResult<IPaginationResponse<AccessTokenLogReadDto>>> GetPaginateableLogs([FromQuery] PaginationRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Ok(await _accessTokensLogsService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        [HttpGet(ApplicationRestAddons.GetById)]
        public async ValueTask<ActionResult<AccessTokenLogReadDto>> Get([FromRoute] int id) =>
            await _accessTokensLogsService.GetTokenLogByIdAsync(id);

        [HttpGet(IdentityServerRoutes.Logs.TokenUsages.ByTokenId)]
        public async ValueTask<IEnumerable<AccessTokenLogReadDto>> GetLogsByTokenId([FromRoute] int id) =>
            await _accessTokensLogsService.GetTokenLogsAsync(id);

        [HttpGet(IdentityServerRoutes.Logs.TokenUsages.ByTokenString)]
        public async ValueTask<IEnumerable<AccessTokenLogReadDto>> GetLogsForTokenByStringToken([FromRoute] int token) =>
            await _accessTokensLogsService.GetTokenLogsAsync(token);

        [HttpGet(IdentityServerRoutes.Logs.TokenUsages.ByUserId)]
        public async ValueTask<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByUserId([FromRoute] int id) =>
            await _accessTokensLogsService.GetUserTokenUsagesByUserIdAsync(id);

        [HttpGet(IdentityServerRoutes.Logs.TokenUsages.ByUserEmail)]
        public async ValueTask<IEnumerable<AccessTokenLogReadDto>> GetAccessTokensLogsByUserEmail([FromRoute] string email) =>
            await _accessTokensLogsService.GetUserTokenUsagesByUserEmailAsync(email);
    }
}
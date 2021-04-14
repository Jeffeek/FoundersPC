#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Tokens
{
    [Route("FoundersPCIdentity/Tokens/Logs")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
    [ApiController]
    public class AccessTokensLogsController : Controller
    {
        private readonly IAccessTokensLogsService _accessTokensLogsService;

        public AccessTokensLogsController(IAccessTokensLogsService accessTokensLogsService) => _accessTokensLogsService = accessTokensLogsService;

        [HttpGet("All")]
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAll() => await _accessTokensLogsService.GetAllTokensLogsAsync();

        [HttpGet]
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetPaginateableLogs([FromQuery(Name = "Page")] int pageNumber,
                                                                                  [FromQuery(Name = "Size")] int pageSize) =>
            await _accessTokensLogsService.GetPaginateableAsync(pageNumber, pageSize);

        [HttpGet("Token/ById/{tokenId:int:min(1)}")]
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetLogsForTokenById([FromRoute] int tokenId) =>
            await _accessTokensLogsService.GetTokenLogsAsync(tokenId);

        [HttpGet("Token/ByToken/{token:length(64)}")]
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetLogsForTokenByStringToken([FromRoute] int token) =>
            await _accessTokensLogsService.GetTokenLogsAsync(token);

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<AccessTokenLogReadDto>> GetByTokenId(int id) => await _accessTokensLogsService.GetTokenLogByIdAsync(id);

        [HttpGet("User/ById/{userId:int:min(1)}")]
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAllUserAccessTokensLogs(int userId) =>
            await _accessTokensLogsService.GetUserTokenUsagesByUserIdAsync(userId);

        [HttpGet("User/ByEmail/{userEmail}")]
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAllUserAccessTokensLogs(string userEmail) =>
            await _accessTokensLogsService.GetUserTokenUsagesByUserEmailAsync(userEmail);
    }
}
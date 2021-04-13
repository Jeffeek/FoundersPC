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
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAll() => await _accessTokensLogsService.GetAllAsync();

        [HttpGet]
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetPaginateableLogs([FromQuery(Name = "Page")] int pageNumber,
                                                                                  [FromQuery(Name = "Size")] int pageSize) =>
            await _accessTokensLogsService.GetPaginateableAsync(pageNumber, pageSize);

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<AccessTokenLogReadDto>> GetByTokenId(int id) => await _accessTokensLogsService.GetByIdAsync(id);

        [HttpGet("User/ById/{userId:int:min(1)}")]
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAllUserAccessTokensLogs(int userId) =>
            await _accessTokensLogsService.GetUserTokenUsagesByUserIdAsync(userId);

        [HttpGet("User/ByEmail/{userEmail}")]
        public async Task<IEnumerable<AccessTokenLogReadDto>> GetAllUserAccessTokensLogs(string userEmail) =>
            await _accessTokensLogsService.GetUserTokenUsagesByUserEmailAsync(userEmail);
    }
}
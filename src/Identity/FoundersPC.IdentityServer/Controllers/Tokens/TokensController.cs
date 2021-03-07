using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.AuthenticationShared;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;

namespace FoundersPC.IdentityServer.Controllers.Tokens
{
    [Route("identityAPI/tokens")]
    [ApiController]
    public class TokensController : Controller
    {
        private readonly IApiAccessUsersTokensService _apiAccessUsersTokensService;

        public TokensController(IApiAccessUsersTokensService apiAccessUsersTokensService)
        {
            _apiAccessUsersTokensService = apiAccessUsersTokensService;
        }

        [HttpGet]
        [Route("user/{userId}")]
        public async Task<ActionResult<IEnumerable<ApiAccessUserTokenReadDto>>> GetUserTokens(int? userId)
        {
            if (!userId.HasValue || userId.Value < 1) return BadRequest();

            var tokens = await _apiAccessUsersTokensService.GetUserTokens(userId.Value);

            return Json(tokens ?? Enumerable.Empty<ApiAccessUserTokenReadDto>());
        }
    }
}

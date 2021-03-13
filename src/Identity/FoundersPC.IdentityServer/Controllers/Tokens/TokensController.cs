#region Using namespaces

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Tokens
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("identityAPI/tokens")]
    [ApiController]
    public class TokensController : Controller
    {
        private readonly IApiAccessUsersTokensService _apiAccessUsersTokensService;

        public TokensController(IApiAccessUsersTokensService apiAccessUsersTokensService) => _apiAccessUsersTokensService = apiAccessUsersTokensService;

        [HttpGet]
        [Route("user/{email}")]
        public async Task<ActionResult<IEnumerable<ApiAccessUserTokenReadDto>>> GetUserTokens(string email)
        {
            var tokens = await _apiAccessUsersTokensService.GetUserTokens(email);

            if (tokens is null)
                return BadRequest(new
                                  {
                                      error = "No user with this email"
                                  });

            return Json(tokens ?? Enumerable.Empty<ApiAccessUserTokenReadDto>());
        }
    }
}
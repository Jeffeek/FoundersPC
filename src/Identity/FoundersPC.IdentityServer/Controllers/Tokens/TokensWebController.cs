#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.WebIdentityShared;
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

        public TokensWebController(IApiAccessUsersTokensService apiAccessUsersTokensService) =>
            _apiAccessUsersTokensService = apiAccessUsersTokensService;

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
    }
}
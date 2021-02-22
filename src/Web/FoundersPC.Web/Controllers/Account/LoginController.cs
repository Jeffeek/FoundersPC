#region Using namespaces

using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.AuthorizationShared;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers.Account
{
    [Controller]
    public class LoginController : Controller
    {
        private readonly TokenConfiguration _settings;

        public LoginController(TokenConfiguration settings)
        {
            _settings = settings;
        }

        [HttpPost]
        public async Task<IActionResult> Authorize(UserAuthorizationRequest authorizationRequest)
        {
            if (!ModelState.IsValid) return ValidationProblem(detail : "Not valid credentials", instance : nameof(authorizationRequest));

            var result = await ApplicationMicroservicesContext.IdentityServerClient.PostAsJsonAsync<UserAuthorizationRequest>("AuthorizationAPI/signin", authorizationRequest);

            if (!result.IsSuccessStatusCode) return NotFound(authorizationRequest);

            var content = await result.Content.ReadFromJsonAsync<UserAuthorizationResponse>();

            return Ok(content);
        }
    }
}
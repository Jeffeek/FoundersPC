#region Using namespaces

using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.AuthorizationShared;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers.Account
{
    [Controller]
    public class RegisterController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationRequest request)
        {
            if (!TryValidateModel(request)) return BadRequest(request);

            var registrationResult =
                await ApplicationMicroservicesContext.IdentityServerClient.PostAsJsonAsync<UserRegistrationRequest>("AuthorizationAPI/signup", request);

            var deliveredResult = await registrationResult.Content.ReadFromJsonAsync<UserRegistrationResponse>();

            if (ReferenceEquals(deliveredResult, null)) throw new ArgumentNullException(nameof(deliveredResult));

            if (deliveredResult.SuccessfulRegistration) return Accepted(deliveredResult);

            return Conflict(deliveredResult);
        }
    }
}
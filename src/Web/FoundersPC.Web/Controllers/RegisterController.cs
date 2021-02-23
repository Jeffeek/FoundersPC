#region Using namespaces

using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.Web.Application.DTO;
using FoundersPC.Web.Application.DTO.Request;
using FoundersPC.Web.Application.DTO.Response;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Controller]
    public class RegisterController : Controller
    {
        public IActionResult RegisterIndex() => View();

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestViewModel request)
        {
            if (!TryValidateModel(request)) return BadRequest(request);

            var registrationResult =
                await ApplicationMicroservicesContext.IdentityServerClient.PostAsJsonAsync<UserRegisterRequestViewModel>("authAPI/registration", request);

            var deliveredResult = await registrationResult.Content.ReadFromJsonAsync<UserRegisterResponse>();

            if (ReferenceEquals(deliveredResult, null)) throw new ArgumentNullException(nameof(deliveredResult));

            if (deliveredResult.IsRegistrationSuccessful) return Accepted(deliveredResult);

            return Conflict(deliveredResult);
        }
    }
}
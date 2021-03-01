#region Using namespaces

using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index() => View();

        [Authorize(Roles = "Administrator")]
        public IActionResult Admin() =>
            Ok(new
               {
                   role = "admin"
               });

        [Authorize(Roles = "Administrator, Manager, DefaultUser")]
        public IActionResult Authenticated() =>
            Ok(new
               {
                   role = "Authenticated"
               });

        [AllowAnonymous]
        public IActionResult All() =>
            Ok(new
               {
                   role = "None"
               });

        [AllowAnonymous]
        public IActionResult AccessDenied() => View("AccessDenied");

        [Authorize]
        public async Task<ActionResult> Cases()
        {
            if (!HttpContext.Request.Cookies.TryGetValue("token", out var token))
                return BadRequest(new
                                  {
                                      error = "No tokens"
                                  });

            ApplicationMicroservicesContext.HardwareApiClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,
                                              token);

            var request = await ApplicationMicroservicesContext.HardwareApiClient.GetAsync("cases");

            var response = await request.Content.ReadAsStringAsync();

            return Ok(response);
        }
    }
}
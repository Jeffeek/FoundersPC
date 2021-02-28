#region Using namespaces

using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.AuthenticationShared;
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
            var roleRequest = new UserRoleRequest()
                              {
                                  Role = HttpContext.User.FindFirst(x => x.Type == "Role")?.Value
                              };

            var request = await ApplicationMicroservicesContext.HardwareApiClient.GetAsync("cases");

            var response = await request.Content.ReadAsStringAsync();

            return Ok(response);
        }
    }
}
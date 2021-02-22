#region Using namespaces

using System.Net.Http.Json;
using System.Threading.Tasks;
using FoundersPC.AuthorizationShared;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    public sealed class HomeController : Controller
    {
        public HomeController() { }

        public ActionResult Index() => View();

        public async Task<ActionResult<UserAuthorizationResponse>> Authenticate(UserAuthorizationRequest request)
        {
            var user =
                await ApplicationMicroservicesContext.IdentityServerClient.GetFromJsonAsync<UserAuthorizationResponse>("AuthorizationAPI/signin");

            if (ReferenceEquals(user, null)) return NotFound(request);

            if (user.IsUserBlocked) return Problem(detail : "User us blocked");

            ViewBag.Token = user.Token;

            return View();
        }
    }
}
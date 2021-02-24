#region Using namespaces

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
    }
}
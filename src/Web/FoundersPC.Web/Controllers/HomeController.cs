#region Using namespaces

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        [AllowAnonymous]
        public IActionResult Index() => View();

        [AllowAnonymous]
        public IActionResult Pricing() => View("Pricing");

        [Authorize]
        public ActionResult SpaceInvaders() => View("Space-Invaders/SpaceInvaders");
    }
}
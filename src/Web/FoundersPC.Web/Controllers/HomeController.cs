#region Using namespaces

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public IActionResult Index() =>
            View();

        public IActionResult Pricing() =>
            View("Pricing");

        public IActionResult Privacy() =>
            View("Privacy");

        public IActionResult About() =>
            View("About");

        public ActionResult SpaceInvaders() =>
            View("Space-Invaders/SpaceInvaders");
    }
}
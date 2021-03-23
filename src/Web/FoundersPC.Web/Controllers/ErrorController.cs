#region Using namespaces

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("Forbidden")]
        [AllowAnonymous]
        public IActionResult ForbiddenIndex() => View("Forbidden");

        [Route("NotFound")]
        [AllowAnonymous]
        public IActionResult NotFoundIndex() => View("NotFound");

        [Route("{statusCode:int}")]
        [AllowAnonymous]
        public IActionResult Error(int statusCode) =>
            statusCode switch
            {
                404 => RedirectToAction("NotFoundIndex"),
                403 => RedirectToAction("ForbiddenIndex"),
                _   => RedirectToAction("Index", "Home")
            };
    }
}
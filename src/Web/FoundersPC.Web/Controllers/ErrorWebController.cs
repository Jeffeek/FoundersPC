#region Using namespaces

using FoundersPC.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Route("Error")]
    public class ErrorWebController : Controller
    {
        [Route("403")]
        [Route("Forbidden")]
        [AllowAnonymous]
        public IActionResult ForbiddenIndex() =>
            View("Error",
                 new ErrorViewModel(403, "Access forbidden"));

        [Route("404")]
        [Route("NotFound")]
        [AllowAnonymous]
        public IActionResult NotFoundIndex() =>
            View("Error",
                 new ErrorViewModel(404, "Not found"));

        [Route("400")]
        [Route("BadRequest")]
        [AllowAnonymous]
        public IActionResult BadRequestIndex() =>
            View("Error",
                 new ErrorViewModel(400, "Bad request"));

        [Route("401")]
        [Route("UserBlocked")]
        [AllowAnonymous]
        public IActionResult BlockedIndex() =>
            View("Error",
                 new ErrorViewModel(401, "User is blocked or not exist"));

        [Route("UserNotFound")]
        [AllowAnonymous]
        public IActionResult UserNotFound() =>
            View("Error",
                 new ErrorViewModel(404, "We didn't found the user with your credentials :("));

        [Route("{statusCode:int?}")]
        [AllowAnonymous]
        public IActionResult Error(int? statusCode) =>
            (statusCode ?? -1) switch
            {
                400 => RedirectToAction("BadRequestIndex"),
                401 => RedirectToAction("BlockedIndex"),
                403 => RedirectToAction("ForbiddenIndex"),
                404 => RedirectToAction("NotFoundIndex"),
                _   => RedirectToAction("Index", "HomeWeb")
            };
    }
}
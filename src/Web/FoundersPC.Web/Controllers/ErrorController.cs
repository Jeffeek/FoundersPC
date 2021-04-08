#region Using namespaces

using FoundersPC.Web.Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [AllowAnonymous]
    [Route("Error")]
    public class ErrorController : Controller
    {
        [Route("403")]
        [Route("Forbidden")]
        public IActionResult ForbiddenIndex() =>
            View("Error",
                 new ErrorViewModel(403, "Access forbidden!"));

        [Route("404")]
        [Route("NotFound")]
        public IActionResult NotFoundIndex() =>
            View("Error",
                 new ErrorViewModel(404, "Not found!"));

        [Route("400")]
        [Route("BadRequest")]
        public IActionResult BadRequestIndex() =>
            View("Error",
                 new ErrorViewModel(400, "Bad request!"));

        [Route("401")]
        [Route("UserBlocked")]
        public IActionResult UnauthorizedIndex() =>
            View("Error",
                 new ErrorViewModel(401, "Unauthorized!"));

        [Route("UserNotFound")]
        public IActionResult UserNotFound() =>
            View("Error",
                 new ErrorViewModel(404, "We didn't found the user with your credentials :("));

        [Route("ServerError")]
        public IActionResult ServerErrorIndex() => View("Error", new ErrorViewModel(500, "Server error :("));

        [Route("UnprocessableIndex")]
        public IActionResult UnprocessableIndex() =>
            View("Error", new ErrorViewModel(422, "Unprocessable operation or object :("));

        [Route("{statusCode:int?}")]
        public IActionResult Error(int? statusCode) =>
            (statusCode ?? -1) switch
            {
                400 => RedirectToAction("BadRequestIndex"),
                401 => RedirectToAction("UnauthorizedIndex"),
                403 => RedirectToAction("ForbiddenIndex"),
                404 => RedirectToAction("NotFoundIndex"),
                500 => RedirectToAction("ServerErrorIndex"),
                422 => RedirectToAction("UnprocessableIndex"),
                _   => View("Error", new ErrorViewModel("Unknown error!"))
            };
    }
}
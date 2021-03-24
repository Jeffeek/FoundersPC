#region Using namespaces

using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using FoundersPC.Web.Models;
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
        public IActionResult ForbiddenIndex() => View("Error", new ErrorViewModel(403, "Access forbidden"));

        [Route("NotFound")]
        [AllowAnonymous]
        public IActionResult NotFoundIndex() => View("Error", new ErrorViewModel(404, "Not found"));

        [Route("BadRequest")]
        [AllowAnonymous]
        public IActionResult BadRequestIndex() => View("Error", new ErrorViewModel(400, "Bad request"));

        [Route("UserBlocked")]
        [AllowAnonymous]
        public IActionResult BlockedIndex() => View("Error", new ErrorViewModel(1337, "User is blocked or not exist"));

        [Route("UserNotFound")]
        [AllowAnonymous]
        public IActionResult UserNotFound(SignInViewModel model) =>
            View("Error", new ErrorViewModel(404, model.LoginOrEmail));

        [Route("{statusCode:int}")]
        [AllowAnonymous]
        public IActionResult Error(int statusCode) =>
            statusCode switch
            {
                400 => RedirectToAction("BadRequestIndex"),
                401 => RedirectToAction("BlockedIndex"),
                403 => RedirectToAction("ForbiddenIndex"),
                404 => RedirectToAction("NotFoundIndex"),
                _   => RedirectToAction("Index", "Home")
            };
    }
}
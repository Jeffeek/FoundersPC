using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace FoundersPC.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index() => View();

        [Authorize(Roles = "Administrator")]
        public IActionResult Admin()
        {
            return Ok(new
                      {
                          role = "admin"
                      });
        }

        [Authorize(Roles = "Administrator, Manager, DefaultUser")]
        public IActionResult Authenticated()
        {
            return Ok(new
                      {
                          role = "Authenticated"
                      });
        }

        [AllowAnonymous]
        public IActionResult All()
        {
            return Ok(new
                      {
                          role = "None"
                      });
        }
    }
}

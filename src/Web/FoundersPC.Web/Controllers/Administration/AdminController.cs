using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using Humanizer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.Web.Controllers.Administration
{
    // todo: add manager service
    [Authorize(Roles = "Administrator")]
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #region Redirection to view

        [Route("userstable")]
        public async Task<ActionResult> Users()
        {
            var token = GetJwtToken();

            if (token is null) throw new CookieException();

            var users = await _adminService.GetAllUsersAsync(token);

            return View(users);
        }

        //public ActionResult RegisterManager() => View();

        //public ActionResult Entrances() => View();

        #endregion

        private string GetJwtToken()
        {
            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            return token;
        }
    }
}

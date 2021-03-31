﻿#region Using namespaces

using System.Net;
using System.Threading.Tasks;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers.Administration
{
    [Authorize(Roles = "Administrator")]
    [Route("Admin")]
    public class AdminWebController : Controller
    {
        private readonly IAdminWebService _adminWebService;

        public AdminWebController(IAdminWebService adminWebService) => _adminWebService = adminWebService;

        [Route("BlockUser")]
        public async Task<ActionResult> BlockUser([FromQuery] int id)
        {
            await _adminWebService.BlockUserByIdAsync(id, GetJwtToken());

            return RedirectToAction("UsersTable", "AdminWeb");
        }

        [Route("UnblockUser")]
        public async Task<ActionResult> UnblockUser([FromQuery] int id)
        {
            await _adminWebService.UnblockUserByIdAsync(id, GetJwtToken());

            return RedirectToAction("UsersTable", "AdminWeb");
        }

        [Route("MakeUserInactive")]
        public async Task<ActionResult> MakeUserInactive([FromQuery] int id)
        {
            await _adminWebService.MakeUserInactiveByIdAsync(id, GetJwtToken());

            return RedirectToAction("UsersTable", "AdminWeb");
        }

        [Route("RegisterManager")]
        [HttpPost]
        public async Task<ActionResult> RegisterManager([FromForm] SignUpViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _adminWebService.RegisterNewManagerAsync(model, GetJwtToken());

            return result ? Ok() : Problem();
        }

        private string GetJwtToken()
        {
            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            return token;
        }

        #region Redirection to view

        [HttpGet]
        [Route("UsersTable")]
        public async Task<ActionResult> UsersTable()
        {
            var token = GetJwtToken();

            if (token is null) throw new CookieException();

            var users = await _adminWebService.GetAllUsersAsync(token);

            return View(users);
        }

        [Route("RegisterManager")]
        [HttpGet]
        public ActionResult RegisterManager() => View();

        //public ActionResult Entrances() => View();

        #endregion
    }
}
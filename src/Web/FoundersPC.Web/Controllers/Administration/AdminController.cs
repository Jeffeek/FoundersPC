using System.Net;
using System.Threading.Tasks;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.Web.Controllers.Administration
{
    // todo: add manager service
    [Authorize(Roles = "Administrator")]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        #region Redirection to view

        [HttpGet]
        [Route("UsersTable")]
        public async Task<ActionResult> UsersTable()
        {
            var token = GetJwtToken();

            if (token is null) throw new CookieException();

            var users = await _adminService.GetAllUsersAsync(token);

            return View(users);
        }

        [HttpGet]
        public ActionResult RegisterManager() => View();

        //public ActionResult Entrances() => View();

        #endregion

        [Route("BlockUser")]
        public async Task<ActionResult> BlockUser([FromQuery] int id)
        {
            await _adminService.BlockUserByIdAsync(id, GetJwtToken());

            return RedirectToAction("UsersTable", "Admin");
        }

        [Route("UnblockUser")]
        public async Task<ActionResult> UnblockUser([FromQuery] int id)
        {
            await _adminService.UnblockUserByIdAsync(id, GetJwtToken());

            return RedirectToAction("UsersTable", "Admin");
        }

        [HttpPost]
        public async Task<ActionResult> RegisterManager(SignUpViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _adminService.RegisterNewManagerAsync(model, GetJwtToken());

            return result ? Ok() : Problem();
        }

        private string GetJwtToken()
        {
            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            return token;
        }
    }
}

#region Using namespaces

using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Dto;
using FoundersPC.Web.Application;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.Web.Domain.Entities.ViewModels.Authentication;
using FoundersPC.Web.Domain.Entities.ViewModels.Entrances;
using FoundersPC.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminWebService _adminWebService;

        public AdminController(IAdminWebService adminWebService) => _adminWebService = adminWebService;

        #region Users Table

        [Route("UsersTable")]
        public async Task<ActionResult> UsersTable([FromQuery] int pageNumber)
        {
            var token = HttpContext.GetJwtTokenFromCookie();

            if (token is null) throw new CookieException();

            var users = (await _adminWebService.GetPaginateableUsersAsync(pageNumber, 2, token)).ToArray();

            var indexModel = new IndexViewModel<UserEntityReadDto>()
                             {
                                 Models = users,
                                 Page = new PageViewModel(pageNumber, users.Length > 0)
                             };

            return View("UsersTable", indexModel);
        }

        #endregion

        #region Register Manager

        [HttpPost("RegisterManager")]
        public async Task<ActionResult> RegisterManager([FromForm] SignUpViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _adminWebService.RegisterNewManagerAsync(model, HttpContext.GetJwtTokenFromCookie());

            return result ? Ok() : Problem();
        }

        [Route("RegisterManager")]
        public ActionResult RegisterManager() => View();

        #endregion

        #region Entrances

        [Route("Entrances")]
        public async Task<ActionResult> Entrances()
        {
            var token = HttpContext.GetJwtTokenFromCookie();

            if (token is null) throw new CookieException();

            var entrances = await _adminWebService.GetAllEntrancesAsync(token);

            var viewModel = new EntrancesViewModel
                            {
                                BetweenFilter = new EntrancesBetweenFilter(),
                                Entrances = entrances,
                                IsDatePickerRequired = true
                            };

            return View("Entrances", viewModel);
        }

        [Route("User/{userId:int}/Entrances")]
        public async Task<ActionResult> UserEntrances([FromRoute] int userId)
        {
            var token = HttpContext.GetJwtTokenFromCookie();

            if (token is null) throw new CookieException();

            var entrances = await _adminWebService.GetAllUserEntrancesAsync(userId, token);

            var viewModel = new EntrancesViewModel
                            {
                                BetweenFilter = new EntrancesBetweenFilter(),
                                Entrances = entrances,
                                IsDatePickerRequired = false
                            };

            return View("Entrances", viewModel);
        }

        [Route("Entrances/Between")]
        public async Task<ActionResult> EntrancesBetween([FromForm] EntrancesViewModel viewModel)
        {
            var token = HttpContext.GetJwtTokenFromCookie();

            if (token is null) throw new CookieException();

            if (viewModel.BetweenFilter is null) return BadRequest();

            if (viewModel.BetweenFilter.Start > viewModel.BetweenFilter.Finish) return BadRequest();

            var entrances = await _adminWebService.GetAllEntrancesBetweenAsync(viewModel.BetweenFilter.Start,
                                                                               viewModel.BetweenFilter.Finish,
                                                                               token);

            var newViewModel = new EntrancesViewModel
                               {
                                   BetweenFilter = viewModel.BetweenFilter,
                                   Entrances = entrances,
                                   IsDatePickerRequired = false
                               };

            return View("Entrances", newViewModel);
        }

        #endregion

        #region Change Users Statuses

        [Route("BlockUser/{id:int}")]
        public async Task<ActionResult> BlockUser([FromRoute] int id)
        {
            await _adminWebService.BlockUserByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            return RedirectToAction("UsersTable", "Admin");
        }

        [Route("UnblockUser/{id:int}")]
        public async Task<ActionResult> UnblockUser([FromRoute] int id)
        {
            await _adminWebService.UnblockUserByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            return RedirectToAction("UsersTable", "Admin");
        }

        [Route("MakeUserInactive/{id:int}")]
        public async Task<ActionResult> MakeUserInactive([FromRoute] int id)
        {
            await _adminWebService.MakeUserInactiveByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            return RedirectToAction("UsersTable", "Admin");
        }

        #endregion
    }
}
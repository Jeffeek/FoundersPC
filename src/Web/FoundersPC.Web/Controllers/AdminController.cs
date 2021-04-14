#region Using namespaces

using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Dto;
using FoundersPC.Web.Application;
using FoundersPC.Web.Application.Interfaces.Services.IdentityServer.Admin_services;
using FoundersPC.Web.Domain.Common;
using FoundersPC.Web.Domain.Common.Authentication;
using FoundersPC.Web.Domain.Common.Entrances;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers
{
    [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService) => _adminService = adminService;

        #region Users Table

        [Route("UsersTable")]
        public async Task<ActionResult> UsersTable([FromQuery] int pageNumber)
        {
            var users = (await _adminService.GetPaginateableUsersAsync(pageNumber,
                                                                       FoundersPCConstants.PageSize,
                                                                       HttpContext.GetJwtTokenFromCookie()))
                .ToArray();

            var indexModel = new IndexViewModel<UserEntityReadDto>
                             {
                                 Models = users,
                                 Page = new PageViewModel(pageNumber,
                                                          users.Length == FoundersPCConstants.PageSize),
                                 IsPaginationNeeded = true
                             };

            return View("UsersTable", indexModel);
        }

        #endregion

        #region Register Manager

        [HttpPost("RegisterManager")]
        public async Task<ActionResult> RegisterManager([FromForm] SignUpViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _adminService.RegisterNewManagerAsync(model, HttpContext.GetJwtTokenFromCookie());

            return result ? Ok() : Problem();
        }

        [Route("RegisterManager")]
        public ActionResult RegisterManager() => View("RegisterManagerPage");

        #endregion

        #region Entrances

        [Route("Entrances")]
        public async Task<ActionResult> EntrancesTable([FromQuery] int pageNumber)
        {
            var entrances =
                (await _adminService.GetPaginateableEntrancesAsync(pageNumber,
                                                                   FoundersPCConstants.PageSize,
                                                                   HttpContext.GetJwtTokenFromCookie()))
                .ToArray();

            var viewModel = new EntrancesViewModel
                            {
                                BetweenFilter = new EntrancesBetweenFilter(),
                                IsDatePickerRequired = true,
                                IndexModel = new IndexViewModel<UserEntranceLogReadDto>()
                                             {
                                                 IsPaginationNeeded = true,
                                                 Models = entrances,
                                                 Page = new PageViewModel(pageNumber, entrances.Length == FoundersPCConstants.PageSize)
                                             }
                            };

            return View("EntrancesTable", viewModel);
        }

        [Route("User/{userId:int}/Entrances")]
        public async Task<ActionResult> UserEntrances([FromRoute] int userId)
        {
            var token = HttpContext.GetJwtTokenFromCookie();

            if (token is null)
                throw new CookieException();

            var entrances = await _adminService.GetAllUserEntrancesByIdAsync(userId, token);

            var viewModel = new EntrancesViewModel
                            {
                                BetweenFilter = null,
                                IsDatePickerRequired = false,
                                IndexModel = new IndexViewModel<UserEntranceLogReadDto>()
                                             {
                                                 IsPaginationNeeded = false,
                                                 Models = entrances,
                                                 Page = new PageViewModel(1, false)
                                             }
                            };

            return View("EntrancesTable", viewModel);
        }

        // todo: make really paging
        [Route("Entrances/Between")]
        public async Task<ActionResult> EntrancesBetween([FromForm] EntrancesViewModel viewModel)
        {
            var token = HttpContext.GetJwtTokenFromCookie();

            if (token is null)
                throw new CookieException();

            if (viewModel.BetweenFilter is null)
                return BadRequest();

            if (viewModel.BetweenFilter.Start > viewModel.BetweenFilter.Finish)
                return BadRequest();

            var entrances = await _adminService.GetAllEntrancesBetweenAsync(viewModel.BetweenFilter.Start,
                                                                            viewModel.BetweenFilter.Finish,
                                                                            token);

            var newViewModel = new EntrancesViewModel()
                               {
                                   BetweenFilter = viewModel.BetweenFilter,
                                   IsDatePickerRequired = false,
                                   IndexModel = new IndexViewModel<UserEntranceLogReadDto>()
                                                {
                                                    Models = entrances,
                                                    IsPaginationNeeded = false,
                                                    Page = new PageViewModel(1, false)
                                                }
                               };

            return View("EntrancesTable", newViewModel);
        }

        #endregion

        #region Access Tokens Logs

        [Route("TokensLogs")]
        public async Task<ActionResult> TokensLogsTable([FromQuery] int pageNumber)
        {
            var logs =
                (await _adminService.GetPaginateableAccessTokensLogsAsync(pageNumber,
                                                                          FoundersPCConstants.PageSize,
                                                                          HttpContext.GetJwtTokenFromCookie()))
                .ToArray();

            var viewModel = new IndexViewModel<AccessTokenLogReadDto>
                            {
                                Models = logs,
                                Page = new PageViewModel(pageNumber, logs.Length == FoundersPCConstants.PageSize),
                                IsPaginationNeeded = true
                            };

            return View("TokensLogsTable", viewModel);
        }

        [Route("User/{userId:int}/TokensLogs")]
        public async Task<ActionResult> UserTokensLogs([FromRoute] int userId)
        {
            var logs = await _adminService.GetAccessTokensLogsByUserIdAsync(userId, HttpContext.GetJwtTokenFromCookie());

            var viewModel = new IndexViewModel<AccessTokenLogReadDto>
                            {
                                Models = logs,
                                Page = new PageViewModel(1, false),
                                IsPaginationNeeded = false
                            };

            return View("TokensLogsTable", viewModel);
        }

        [Route("TokensLogs/ByTokenId/{tokenId:int:min(1)}")]
        public async Task<ActionResult> TokenLogs([FromRoute] int tokenId)
        {
            var logs =
                (await _adminService.GetAccessTokensLogsByTokenIdAsync(tokenId,
                                                                       HttpContext.GetJwtTokenFromCookie()))
                .ToArray();

            var viewModel = new IndexViewModel<AccessTokenLogReadDto>
                            {
                                Models = logs,
                                Page = new PageViewModel(1, false),
                                IsPaginationNeeded = false
                            };

            return View("TokensLogsTable", viewModel);
        }

        [Route("TokensLogs/ByToken/{token:length(64)}")]
        public async Task<ActionResult> TokenLogs([FromRoute] string token)
        {
            var logs =
                (await _adminService.GetAccessTokensLogsByTokenAsync(token,
                                                                     HttpContext.GetJwtTokenFromCookie()))
                .ToArray();

            var viewModel = new IndexViewModel<AccessTokenLogReadDto>
                            {
                                Models = logs,
                                Page = new PageViewModel(1, false),
                                IsPaginationNeeded = false
                            };

            return View("TokensLogsTable", viewModel);
        }

        #endregion

        #region Access Tokens

        [Route("Tokens")]
        public async Task<ActionResult> TokensTable([FromQuery] int pageNumber)
        {
            var tokens =
                (await _adminService.GetPaginateableTokensAsync(pageNumber,
                                                                FoundersPCConstants.PageSize,
                                                                HttpContext.GetJwtTokenFromCookie()))
                .ToArray();

            var viewModel = new IndexViewModel<ApiAccessUserTokenReadDto>
                            {
                                Models = tokens,
                                Page = new PageViewModel(pageNumber,
                                                         tokens.Length == FoundersPCConstants.PageSize),
                                IsPaginationNeeded = true
                            };

            return View("TokensTable", viewModel);
        }

        [Route("Tokens/Block/{tokenId:int:min(1)}")]
        public async Task<ActionResult> BlockToken([FromRoute] int tokenId)
        {
            var blockResult = await _adminService.BlockTokenByIdAsync(tokenId, HttpContext.GetJwtTokenFromCookie());

            if (blockResult)
                return await TokensTable(1);

            return BadRequest();
        }

        #endregion

        #region Change Users Statuses

        [Route("BlockUser/{id:int}")]
        public async Task<ActionResult> BlockUser([FromRoute] int id)
        {
            await _adminService.BlockUserByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            return RedirectToAction("UsersTable", "Admin");
        }

        [Route("UnblockUser/{id:int}")]
        public async Task<ActionResult> UnblockUser([FromRoute] int id)
        {
            await _adminService.UnblockUserByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            return RedirectToAction("UsersTable", "Admin");
        }

        [Route("MakeUserInactive/{id:int}")]
        public async Task<ActionResult> MakeUserInactive([FromRoute] int id)
        {
            await _adminService.MakeUserInactiveByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            return RedirectToAction("UsersTable", "Admin");
        }

        #endregion
    }
}
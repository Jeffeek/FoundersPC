#region Using namespaces

using System.Linq;
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
using PagedList.Core;

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
            var paginationResponse = await _adminService.GetPaginateableUsersAsync(pageNumber,
                                                                                   FoundersPCConstants.PageSize,
                                                                                   HttpContext.GetJwtTokenFromCookie());

            var indexModel = new IndexViewModel<UserEntityReadDto>
                             {
                                 PagedList = new StaticPagedList<UserEntityReadDto>(paginationResponse.Items,
                                                                                    pageNumber,
                                                                                    FoundersPCConstants.PageSize,
                                                                                    paginationResponse.TotalItemsCount),
                                 IsPaginationNeeded = true
                             };

            return View("UsersTable", indexModel);
        }

        #region Change Users Statuses

        [Route("BlockUser/{id:int:min(1)}")]
        public async Task<ActionResult> BlockUser([FromRoute] int id)
        {
            await _adminService.BlockUserByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            return RedirectToAction("UsersTable", "Admin");
        }

        [Route("UnblockUser/{id:int:min(1)}")]
        public async Task<ActionResult> UnblockUser([FromRoute] int id)
        {
            await _adminService.UnblockUserByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            return RedirectToAction("UsersTable", "Admin");
        }

        [Route("MakeUserInactive/{id:int:min(1)}")]
        public async Task<ActionResult> MakeUserInactive([FromRoute] int id)
        {
            await _adminService.MakeUserInactiveByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            return RedirectToAction("UsersTable", "Admin");
        }

        #endregion

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

        [Route("EntrancesTable")]
        public async Task<ActionResult> EntrancesTable([FromQuery] int pageNumber)
        {
            var paginationResponse =
                await _adminService.GetPaginateableEntrancesAsync(pageNumber,
                                                                  FoundersPCConstants.PageSize,
                                                                  HttpContext.GetJwtTokenFromCookie());

            var viewModel = new EntrancesViewModel
                            {
                                BetweenFilter = new EntrancesBetweenFilter(),
                                IsDatePickerRequired = true,
                                IndexModel = new IndexViewModel<UserEntranceLogReadDto>
                                             {
                                                 IsPaginationNeeded = true,
                                                 PagedList = new StaticPagedList<UserEntranceLogReadDto>(paginationResponse.Items,
                                                                                                         pageNumber,
                                                                                                         FoundersPCConstants.PageSize,
                                                                                                         paginationResponse.TotalItemsCount)
                                             }
                            };

            return View("EntrancesTable", viewModel);
        }

        [Route("User/{userId:int}/Entrances")]
        public async Task<ActionResult> UserEntrances([FromRoute] int userId)
        {
            var entrances = (await _adminService.GetAllUserEntrancesByIdAsync(userId, HttpContext.GetJwtTokenFromCookie())).ToArray();

            var length = entrances.Length == 0 ? 1 : entrances.Length;

            var viewModel = new EntrancesViewModel
                            {
                                BetweenFilter = null,
                                IsDatePickerRequired = false,
                                IndexModel = new IndexViewModel<UserEntranceLogReadDto>
                                             {
                                                 IsPaginationNeeded = false,
                                                 PagedList = new StaticPagedList<UserEntranceLogReadDto>(entrances, 1, length, length)
                                             }
                            };

            return View("EntrancesTable", viewModel);
        }

        [Route("Entrances/Between")]
        public async Task<ActionResult> EntrancesBetween([FromForm] EntrancesViewModel viewModel)
        {
            if (viewModel.BetweenFilter is null)
                return BadRequest();

            if (viewModel.BetweenFilter.Start > viewModel.BetweenFilter.Finish)
                return BadRequest();

            var entrances = (await _adminService.GetAllEntrancesBetweenAsync(viewModel.BetweenFilter.Start,
                                                                             viewModel.BetweenFilter.Finish,
                                                                             HttpContext.GetJwtTokenFromCookie())).ToArray();

            var length = entrances.Length == 0 ? 1 : entrances.Length;

            var newViewModel = new EntrancesViewModel
                               {
                                   BetweenFilter = viewModel.BetweenFilter,
                                   IsDatePickerRequired = false,
                                   IndexModel = new IndexViewModel<UserEntranceLogReadDto>
                                                {
                                                    PagedList = new StaticPagedList<UserEntranceLogReadDto>(entrances, 1, length, length),
                                                    IsPaginationNeeded = false
                                                }
                               };

            return View("EntrancesTable", newViewModel);
        }

        #endregion

        #region Access Tokens Logs

        [Route("TokensLogsTable")]
        public async Task<ActionResult> TokensLogsTable([FromQuery] int pageNumber)
        {
            var logs =
                await _adminService.GetPaginateableAccessTokensLogsAsync(pageNumber,
                                                                         FoundersPCConstants.PageSize,
                                                                         HttpContext.GetJwtTokenFromCookie());

            var viewModel = new IndexViewModel<AccessTokenLogReadDto>
                            {
                                PagedList = new StaticPagedList<AccessTokenLogReadDto>(logs.Items,
                                                                                       pageNumber,
                                                                                       FoundersPCConstants.PageSize,
                                                                                       logs.TotalItemsCount),
                                IsPaginationNeeded = true
                            };

            return View("TokensLogsTable", viewModel);
        }

        [Route("User/{userId:int:min(1)}/TokensLogsTable")]
        public async Task<ActionResult> UserTokensLogs([FromRoute] int userId)
        {
            var logs = (await _adminService.GetAccessTokensLogsByUserIdAsync(userId, HttpContext.GetJwtTokenFromCookie())).ToArray();

            var length = logs.Length == 0 ? 1 : logs.Length;

            var viewModel = new IndexViewModel<AccessTokenLogReadDto>
                            {
                                PagedList = new StaticPagedList<AccessTokenLogReadDto>(logs, 1, length, length),
                                IsPaginationNeeded = false
                            };

            return View("TokensLogsTable", viewModel);
        }

        [Route("TokensLogsTable/ByTokenId/{tokenId:int:min(1)}")]
        public async Task<ActionResult> TokenLogs([FromRoute] int tokenId)
        {
            var logs =
                (await _adminService.GetAccessTokensLogsByTokenIdAsync(tokenId,
                                                                       HttpContext.GetJwtTokenFromCookie()))
                .ToArray();

            var length = logs.Length == 0 ? 1 : logs.Length;

            var viewModel = new IndexViewModel<AccessTokenLogReadDto>
                            {
                                PagedList = new StaticPagedList<AccessTokenLogReadDto>(logs, 1, length, length),
                                IsPaginationNeeded = false
                            };

            return View("TokensLogsTable", viewModel);
        }

        [Route("TokensLogsTable/ByToken/{token:length(64)}")]
        public async Task<ActionResult> TokenLogs([FromRoute] string token)
        {
            var logs =
                (await _adminService.GetAccessTokensLogsByTokenAsync(token,
                                                                     HttpContext.GetJwtTokenFromCookie()))
                .ToArray();

            var length = logs.Length == 0 ? 1 : logs.Length;

            var viewModel = new IndexViewModel<AccessTokenLogReadDto>
                            {
                                PagedList = new StaticPagedList<AccessTokenLogReadDto>(logs, 1, length, length),
                                IsPaginationNeeded = false
                            };

            return View("TokensLogsTable", viewModel);
        }

        #endregion

        #region Access Tokens

        [Route("TokensTable")]
        public async Task<ActionResult> TokensTable([FromQuery] int pageNumber)
        {
            var tokens =
                await _adminService.GetPaginateableTokensAsync(pageNumber,
                                                               FoundersPCConstants.PageSize,
                                                               HttpContext.GetJwtTokenFromCookie());

            var viewModel = new IndexViewModel<AccessUserTokenReadDto>
                            {
                                PagedList = new StaticPagedList<AccessUserTokenReadDto>(tokens.Items,
                                                                                        pageNumber,
                                                                                        FoundersPCConstants.PageSize,
                                                                                        tokens.TotalItemsCount),
                                IsPaginationNeeded = true
                            };

            return View("TokensTable", viewModel);
        }

        [Route("TokensTable/Block/{tokenId:int:min(1)}")]
        public async Task<ActionResult> BlockToken([FromRoute] int tokenId)
        {
            var blockResult = await _adminService.BlockTokenByIdAsync(tokenId, HttpContext.GetJwtTokenFromCookie());

            if (blockResult)
                return await TokensTable(1);

            return BadRequest();
        }

        #endregion
    }
}
#region Using namespaces

using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Users.Blocking;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Users.Inactivity;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Users.Unblocking;
using FoundersPC.RequestResponseShared.Response.Administration.Admin.Users.Blocking;
using FoundersPC.RequestResponseShared.Response.Administration.Admin.Users.Inactivity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Users.AdminSection
{
    [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
    [Route("FoundersPCIdentity/Users/StatusChange")]
    [ApiController]
    public class UserStatusController : Controller
    {
        private readonly IAdminService _adminService;

        public UserStatusController(IAdminService adminService) => _adminService = adminService;

        [HttpPut("Block/ById")]
        public async Task<ActionResult<BlockUserResponse>> BlockUser([FromBody] BlockUserByIdRequest byIdRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            if (byIdRequest.UserId < 1)
                return BadRequest(new
                                  {
                                      error = $"Bad id. Id was {byIdRequest.UserId}, expected > 1"
                                  });

            var blockUserResult = await _adminService.BlockUserAsync(byIdRequest.UserId);

            if (blockUserResult)
                return new BlockUserResponse
                       {
                           AdministratorEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
                           IsBlockingSuccessful = true
                       };

            return new BlockUserResponse
                   {
                       AdministratorEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
                       IsBlockingSuccessful = false,
                       Error = $"User with id = {byIdRequest.UserId} was not blocked at this byIdRequest."
                   };
        }

        [HttpPut("Block/ByEmail")]
        public async Task<ActionResult<BlockUserResponse>> BlockUser([FromBody] BlockUserByEmailRequest byEmailRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            if (byEmailRequest.UserEmail is null)
                return BadRequest(new
                                  {
                                      error = "Bad email. Id was was, expected not null"
                                  });

            var blockUserResult =
                await _adminService.BlockUserAsync(byEmailRequest.UserEmail,
                                                   byEmailRequest.BlockUserTokens,
                                                   byEmailRequest.SendNotificationToUserViaEmail);

            if (blockUserResult)
                return new BlockUserResponse
                       {
                           AdministratorEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
                           IsBlockingSuccessful = true
                       };

            return new BlockUserResponse
                   {
                       AdministratorEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
                       IsBlockingSuccessful = false,
                       Error = $"User with email = {byEmailRequest.UserEmail} was not blocked at this byEmailRequest."
                   };
        }

        [HttpPut("UnBlock/ById")]
        public async Task<ActionResult<UnblockUserResponse>> UnBlockUser([FromBody] UnblockUserByIdRequest byIdRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            if (byIdRequest.UserId < 1)
                return BadRequest(new
                                  {
                                      error = $"Bad id. Id was {byIdRequest.UserId}, expected > 1"
                                  });

            var unblockUserResult = await _adminService.UnBlockUserAsync(byIdRequest.UserId);

            if (unblockUserResult)
                return new UnblockUserResponse
                       {
                           AdministratorEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
                           IsUnblockingSuccessful = true
                       };

            return new UnblockUserResponse
                   {
                       AdministratorEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
                       IsUnblockingSuccessful = false,
                       Error = $"User with id = {byIdRequest.UserId} was not unblocked at this byIdRequest."
                   };
        }

        [HttpPut("UnBlock/ByEmail")]
        public async Task<ActionResult<UnblockUserResponse>> UnBlockUser(
            [FromBody] UnblockUserByEmailRequest byEmailRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            var blockUserResult = await _adminService.UnBlockUserAsync(byEmailRequest.UserEmail,
                                                                       byEmailRequest.SendNotificationToUserViaEmail);

            if (blockUserResult)
                return new UnblockUserResponse
                       {
                           AdministratorEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
                           IsUnblockingSuccessful = true
                       };

            return new UnblockUserResponse
                   {
                       AdministratorEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
                       IsUnblockingSuccessful = false,
                       Error = $"User with email = {byEmailRequest.UserEmail} was not unblocked at this byEmailRequest."
                   };
        }

        [HttpDelete("MakeInactive/ByEmail")]
        public async Task<ActionResult<MakeUserInactiveResponse>> MakeUserInactive(
            [FromBody] MakeUserInactiveByEmailRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _adminService.MakeUserInactiveAsync(request.UserEmail);

            return new MakeUserInactiveResponse
                   {
                       AdministratorEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
                       Error = null,
                       IsUserMadeInactiveSuccessful = result
                   };
        }

        [HttpDelete("MakeInactive/ById")]
        public async Task<ActionResult<MakeUserInactiveResponse>> MakeUserInactive(
            [FromBody] MakeUserInactiveByIdRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var result = await _adminService.MakeUserInactiveAsync(request.UserId);

            return new MakeUserInactiveResponse
                   {
                       AdministratorEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
                       Error = null,
                       IsUserMadeInactiveSuccessful = result
                   };
        }
    }
}
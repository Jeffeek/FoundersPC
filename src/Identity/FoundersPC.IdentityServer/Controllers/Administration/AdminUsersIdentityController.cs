#region Using namespaces

using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Users.Blocking;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Users.Inactivity;
using FoundersPC.RequestResponseShared.Request.Administration.Admin.Users.Unblocking;
using FoundersPC.RequestResponseShared.Response.Administration.Admin.Users.Blocking;
using FoundersPC.RequestResponseShared.Response.Administration.Admin.Users.Inactivity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Administration
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
               Roles = ApplicationRoles.Administrator)]
    [ApiController]
    [Route("FoundersPCIdentity/Admin/Users")]
    public class AdminUsersIdentityController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly IUsersInformationService _usersInformationService;

        public AdminUsersIdentityController(IUsersInformationService usersInformationService,
                                            IMapper mapper,
                                            IAdminService adminService)
        {
            _usersInformationService = usersInformationService;
            _mapper = mapper;
            _adminService = adminService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserEntityReadDto>> Get(int id)
        {
            var user = await _usersInformationService.GetUserByIdAsync(id);

            if (user is null) return NotFound();

            return _mapper.Map<UserEntityReadDto, UserEntityReadDto>(user);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserEntityReadDto>> Get(string email)
        {
            if (String.IsNullOrEmpty(email))
                return BadRequest(new
                                  {
                                      error = "email can't be null or empty"
                                  });

            var user = await _usersInformationService.FindUserByEmailAsync(email);

            if (user is null) return NotFound();

            return _mapper.Map<UserEntityReadDto, UserEntityReadDto>(user);
        }

        [HttpGet]
        public async Task<IEnumerable<UserEntityReadDto>> Get()
        {
            var users = await _usersInformationService.GetAllUsersAsync();

            return _mapper.Map<IEnumerable<UserEntityReadDto>, IEnumerable<UserEntityReadDto>>(users);
        }

        [HttpPut("Block/By/Id")]
        public async Task<ActionResult<BlockUserResponse>> BlockUser(BlockUserByIdRequest byIdRequest)
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

        [HttpPut("Block/By/Email")]
        public async Task<ActionResult<BlockUserResponse>> BlockUser(BlockUserByEmailRequest byEmailRequest)
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

        [HttpPut("UnBlock/By/Id")]
        public async Task<ActionResult<UnblockUserResponse>> UnBlockUser(UnblockUserByIdRequest byIdRequest)
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

        [HttpPut("UnBlock/By/Email")]
        public async Task<ActionResult<UnblockUserResponse>> UnBlockUser(UnblockUserByEmailRequest byEmailRequest)
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

        [HttpDelete("Inactive/By/Email")]
        public async Task<ActionResult<MakeUserInactiveResponse>> MakeUserInactive(
            MakeUserInactiveByEmailRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = await _adminService.MakeUserInactiveAsync(request.UserEmail);

            return new MakeUserInactiveResponse
                   {
                       AdministratorEmail = HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType),
                       Error = null,
                       IsUserMadeInactiveSuccessful = result
                   };
        }

        [HttpDelete("Inactive/By/Id")]
        public async Task<ActionResult<MakeUserInactiveResponse>> MakeUserInactive(MakeUserInactiveByIdRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

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
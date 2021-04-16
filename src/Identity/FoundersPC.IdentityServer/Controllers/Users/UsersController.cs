#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Pagination;
using FoundersPC.RequestResponseShared.Pagination.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Users
{
    [EnableCors("WebPolicy")]
    [ApiController]
    [Route(IdentityServerRoutes.Users.UsersEndpoint)]
    public class UsersController : Controller
    {
        private readonly IUsersInformationService _usersInformationService;

        public UsersController(IUsersInformationService usersInformationService) => _usersInformationService = usersInformationService;

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet(IdentityServerRoutes.Users.Get.ById)]
        public async ValueTask<ActionResult<UserEntityReadDto>> GetById([FromRoute] int id)
        {
            var user = await _usersInformationService.GetUserByIdAsync(id);

            if (user is null)
                return NotFound();

            return user;
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
        [HttpGet(IdentityServerRoutes.Users.Get.ByEmail)]
        public async ValueTask<ActionResult<UserEntityReadDto>> GetByEmail([FromRoute] string email)
        {
            if (String.IsNullOrEmpty(email))
                return BadRequest(new
                                  {
                                      error = "email can't be null or empty"
                                  });

            var (jwtEmail, jwtRole) = HttpContext.ParseJwtUserTokenCredentials();

            if (jwtRole is not ApplicationRoles.Administrator
                && email != jwtEmail)
                return Unauthorized();

            var user = await _usersInformationService.FindUserByEmailAsync(email);

            if (user is null)
                return NotFound();

            return user;
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet(ApplicationRestAddons.All)]
        public async ValueTask<IEnumerable<UserEntityReadDto>> GetAll()
        {
            var users = await _usersInformationService.GetAllUsersAsync();

            return users;
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet]
        public async ValueTask<ActionResult<IPaginationResponse<UserEntityReadDto>>> Get([FromQuery] PaginationRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            return Json(await _usersInformationService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }
    }
}
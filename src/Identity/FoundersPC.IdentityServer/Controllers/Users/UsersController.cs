#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.ApplicationShared.Middleware;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Pagination.Requests;
using FoundersPC.RequestResponseShared.Pagination.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Users
{
    [EnableCors("WebPolicy")]
    [ApiController]
    [Route(IdentityServerRoutes.Users.UsersEndpoint)]
    [ModelValidation]
    public class UsersController : Controller
    {
        private readonly IUsersInformationService _usersInformationService;

        public UsersController(IUsersInformationService usersInformationService) =>
            _usersInformationService = usersInformationService;

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet(IdentityServerRoutes.Users.ByUserId)]
        public async ValueTask<ActionResult<UserEntityReadDto>> GetById([FromRoute] int id)
        {
            var user = await _usersInformationService.GetUserByIdAsync(id);

            if (user is null)
                return NotFound();

            return user;
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
        [HttpGet(IdentityServerRoutes.Users.ByUserEmail)]
        public async ValueTask<ActionResult<UserEntityReadDto>> GetByEmail([FromRoute] string email)
        {
            var (jwtEmail, jwtRole) = HttpContext.ParseCredentials();

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
        public async ValueTask<ActionResult<IPaginationResponse<UserEntityReadDto>>> Get([FromQuery] PaginationRequest request) =>
            Json(await _usersInformationService.GetPaginateableAsync(request.PageNumber, request.PageSize));
    }
}
#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Dto;
using FoundersPC.RequestResponseShared.Response.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Users
{
    [EnableCors("WebPolicy")]
    [ApiController]
    [Route("FoundersPCIdentity/Users")]
    public class UsersController : Controller
    {
        private readonly IUsersInformationService _usersInformationService;

        public UsersController(IUsersInformationService usersInformationService) => _usersInformationService = usersInformationService;

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet("ById/{id:int:min(1)}")]
        public async Task<ActionResult<UserEntityReadDto>> GetById([FromRoute] int id)
        {
            var user = await _usersInformationService.GetUserByIdAsync(id);

            if (user is null)
                return NotFound();

            return user;
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
        [HttpGet("ByEmail/{email}")]
        public async Task<ActionResult<UserEntityReadDto>> GetByEmail([FromRoute] string email)
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
        [HttpGet("All")]
        public async Task<IEnumerable<UserEntityReadDto>> GetAll()
        {
            var users = await _usersInformationService.GetAllUsersAsync();

            return users;
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet]
        public async Task<IPaginationResponse<UserEntityReadDto>> Get([FromQuery(Name = "Page")] int pageNumber = 1,
                                                                      [FromQuery(Name = "Size")] int pageSize = FoundersPCConstants.PageSize) =>
            await _usersInformationService.GetPaginateableAsync(pageNumber, pageSize);
    }
}
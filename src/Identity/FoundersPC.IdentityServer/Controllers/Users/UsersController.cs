#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Dto;
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
        private readonly IMapper _mapper;
        private readonly IUsersInformationService _usersInformationService;

        public UsersController(IUsersInformationService usersInformationService,
                               IMapper mapper)
        {
            _usersInformationService = usersInformationService;
            _mapper = mapper;
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet("ById/{id:int:min(1)}")]
        public async Task<ActionResult<UserEntityReadDto>> Get([FromRoute] int id)
        {
            var user = await _usersInformationService.GetUserByIdAsync(id);

            if (user is null)
                return NotFound();

            return user;
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
        [HttpGet("ByEmail/{email}")]
        public async Task<ActionResult<UserEntityReadDto>> Get([FromRoute] string email)
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
        [HttpGet(Order = 1)]
        public async Task<IEnumerable<UserEntityReadDto>> Get()
        {
            var users = await _usersInformationService.GetAllUsersAsync();

            return users;
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet(Order = 0)]
        public async Task<IEnumerable<UserEntityReadDto>> GetWithPagination([FromQuery(Name = "Page")] int pageNumber = 1,
                                                                            [FromQuery(Name = "Size")] int pageSize = 10)
        {
            var takenUsers = await _usersInformationService.GetPaginateableAsync(pageNumber, pageSize);

            return takenUsers;
        }
    }
}
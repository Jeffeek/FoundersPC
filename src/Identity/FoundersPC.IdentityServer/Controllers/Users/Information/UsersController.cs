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
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Users.Information
{
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
        [HttpGet("ById/{id:int}")]
        public async Task<ActionResult<UserEntityReadDto>> Get([FromRoute] int id)
        {
            var user = await _usersInformationService.GetUserByIdAsync(id);

            if (user is null) return NotFound();

            return _mapper.Map<UserEntityReadDto, UserEntityReadDto>(user);
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

            if (jwtRole is not ApplicationRoles.Administrator && email != jwtEmail) return Unauthorized();

            var user = await _usersInformationService.FindUserByEmailAsync(email);

            if (user is null) return NotFound();

            return _mapper.Map<UserEntityReadDto, UserEntityReadDto>(user);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.AdministratorPolicy)]
        [HttpGet]
        public async Task<IEnumerable<UserEntityReadDto>> Get()
        {
            var users = await _usersInformationService.GetAllUsersAsync();

            return _mapper.Map<IEnumerable<UserEntityReadDto>, IEnumerable<UserEntityReadDto>>(users);
        }
    }
}
#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.WebIdentityShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Administration
{
    [Route("identityAPI/admin")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
               Roles = "Administrator")]
    [ApiController]
    public class AdminController : Controller
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public AdminController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [HttpGet("users/{id}")]
        public async Task<ActionResult<ApplicationUser>> Get(int? id)
        {
            if (!id.HasValue
                || id.Value < 1)
                return BadRequest();

            var user = await _usersService.GetByIdAsync(id.Value);

            if (user is null) return NotFound();

            return _mapper.Map<UserEntityReadDto, ApplicationUser>(user);
        }

        [HttpGet("users/{email}")]
        public async Task<ActionResult<ApplicationUser>> Get(string email)
        {
            if (String.IsNullOrEmpty(email))
                return BadRequest(new
                                  {
                                      error = "email can't be null or empty"
                                  });

            var user = await _usersService.FindUserByEmailAsync(email);

            if (user is null) return NotFound();

            return _mapper.Map<UserEntityReadDto, ApplicationUser>(user);
        }

        // todo: bug. object role replace with string
        [HttpGet("users")]
        public async Task<IEnumerable<ApplicationUser>> Get()
        {
            var users = await _usersService.GetAllAsync();

            return _mapper.Map<IEnumerable<UserEntityReadDto>, IEnumerable<ApplicationUser>>(users);
        }
    }
}
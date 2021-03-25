#region Using namespaces

using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.WebIdentityShared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.User.Settings
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("FoundersPCIdentity/User/Settings")]
    [ApiController]
    public class UserSettingsIdentityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUsersInformationService _usersInformationService;

        public UserSettingsIdentityController(IUsersInformationService usersInformationService, IMapper mapper)
        {
            _usersInformationService = usersInformationService;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("OverallInformation/{email}")]
        public async Task<ActionResult<ApplicationUser>> GetOverallInformation(string email)
        {
            if (email is null) return BadRequest();

            var user = await _usersInformationService.FindUserByEmailAsync(email);

            if (User is null) return NotFound();

            return _mapper.Map<UserEntityReadDto, ApplicationUser>(user);
        }
    }
}
#region Using namespaces

using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared;
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
    public class UserSettingsController : Controller
    {
        private readonly IUsersInformationService _usersInformationService;
        private readonly IMapper _mapper;

        public UserSettingsController(IUsersInformationService usersInformationService, IMapper mapper)
        {
            _usersInformationService = usersInformationService;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Notifications/{email}")]
        public async Task<ActionResult> GetUserNotifications(string email)
        {
            if (email is null) return BadRequest();

            var isRequestGranted = false;

            if (HttpContext.User.IsInRole(ApplicationRoles.Administrator.ToString()))
                isRequestGranted = true;
            else if (HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType) == email) isRequestGranted = true;

            if (!isRequestGranted) return Unauthorized();

            var user = await _usersInformationService.FindUserByEmailAsync(email);

            if (user is null) return NotFound();

            return Json(new
                        {
                            SendNotificationOnEntrance = user.SendMessageOnEntrance,
                            SendNotificationOnUsingAPI = user.SendMessageOnApiRequest
                        });
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Login/{email}")]
        public async Task<ActionResult> GetUserLogin(string email)
        {
            if (email is null) return BadRequest();

            var isRequestGranted = false;

            if (HttpContext.User.IsInRole(ApplicationRoles.Administrator.ToString()))
                isRequestGranted = true;
            else if (HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType) == email) isRequestGranted = true;

            if (!isRequestGranted) return Unauthorized();

            var user = await _usersInformationService.FindUserByEmailAsync(email);

            if (user is null) return NotFound();

            return Ok(user.Login);
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
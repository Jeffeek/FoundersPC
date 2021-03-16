#region Using namespaces

using System.Security.Claims;
using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
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

        public UserSettingsController(IUsersInformationService usersInformationService) => _usersInformationService = usersInformationService;

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        [Route("Notifications/{email}")]
        public async Task<IActionResult> GetUserNotifications(string email)
        {
            if (email is null) return BadRequest();

            var isRequestGranted = false;

            if (HttpContext.User.IsInRole(ApplicationRoles.Administrator.ToString()))
                isRequestGranted = true;
            else if (HttpContext.User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType) == email)
                isRequestGranted = true;

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
    }
}
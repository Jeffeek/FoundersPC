#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.User
{
    [Route("identityAPI/user/settings")]
    [ApiController]
    public class UserSettingsController : Controller
    {
        private readonly IUsersService _usersService;

        public UserSettingsController(IUsersService usersService) => _usersService = usersService;

        [HttpGet]
        [Route("notifications/{email}")]
        public async Task<IActionResult> GetUserNotifications(string email)
        {
            var user = await _usersService.FindUserByEmailAsync(email);

            if (user is null) return NotFound();

            return Json(new
                        {
                            SendNotificationOnEntrance = user.SendMessageOnEntrance,
                            SendNotificationOnUsingAPI = user.SendMessageOnApiRequest
                        });
        }

        [HttpGet]
        [Route("login/{email}")]
        public async Task<ActionResult> GetUserLogin(string email)
        {
            var user = await _usersService.FindUserByEmailAsync(email);

            if (user is null) return NotFound();

            return Ok(user.Login);
        }
    }
}
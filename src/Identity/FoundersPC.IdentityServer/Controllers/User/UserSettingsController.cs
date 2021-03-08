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
        [Route("notifications/{userId}")]
        public async Task<IActionResult> GetUserNotifications(int? userId)
        {
            if (!userId.HasValue || userId.Value < 1) return BadRequest();

            var user = await _usersService.GetByIdAsync(userId.Value);

            if (user is null) return NotFound();

            return Json(new
                        {
                            SendNotificationOnEntrance = user.SendMessageOnEntrance,
                            SendNotificationOnUsingAPI = user.SendMessageOnApiRequest
                        });
        }

        [HttpGet]
        [Route("login/{userId}")]
        public async Task<ActionResult> GetUserLogin(int? userId)
        {
            if (!userId.HasValue || userId.Value < 1) return BadRequest();

            var user = await _usersService.GetByIdAsync(userId.Value);

            if (user is null) return NotFound();

            return new ObjectResult(user.Login);
        }
    }
}
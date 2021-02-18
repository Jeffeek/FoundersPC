#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Services.Users.Identity;
using FoundersPC.ApplicationShared.DTO.Users.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserIdentityService _identityService;

        public UsersController(IUserIdentityService identityService) => _identityService = identityService;

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate(UserLoginViewModel loginRequest)
        {
            var resultOfAuthentication = await _identityService.AuthorizeAsync(loginRequest);

            if (!resultOfAuthentication)
                return NotFound(new
                                {
                                    message = "Login or password is incorrect"
                                });

            return Ok();
        }
    }
}
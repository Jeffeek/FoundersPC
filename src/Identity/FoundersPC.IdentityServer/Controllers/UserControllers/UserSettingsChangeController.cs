using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.AuthenticationShared.Request;
using FoundersPC.AuthenticationShared.Response;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;

namespace FoundersPC.IdentityServer.Controllers.UserControllers
{
    [Route("identityAPI/user")]
    [ApiController]
    public class UserSettingsChangeController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UserSettingsChangeController(IUsersService usersService, IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        [Route("changepassword")]
        [HttpPost]
        public async Task<ActionResult<UserChangePasswordResponse>> ChangePassword(UserChangePasswordRequest request)
        {
            if (!TryValidateModel(request))
                return BadRequest(new
                                  {
                                      error = "Bad model"
                                  });

            var result = await _usersService.ChangePasswordToAsync(request.Email, request.NewPassword);

            return new UserChangePasswordResponse()
                   {
                       Email = request.Email,
                       Successful = result
                   };
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.AuthorizationShared;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;

namespace FoundersPC.IdentityServer.Controllers.Authorization
{
    [ApiController]
    [Route("AuthorizationAPI/signup")]
    public class RegistrationController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RegistrationController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<UserRegistrationResponse> TryToRegisterUser(UserRegistrationRequest request)
        {
            if (ReferenceEquals(request, null)) return null;

            var result = await _userService.TryToRegisterUser(request.Email, request.RawPassword);

            if (result)
                return new UserRegistrationResponse()
                       {
                           Email = request.Email,
                           RoleTitle = "DefaultUser",
                           SuccessfulRegistration = true
                       };

            return new UserRegistrationResponse()
                   {
                       Email = request.Email,
                       RoleTitle = "None",
                       SuccessfulRegistration = false
                   };
        }
    }
}

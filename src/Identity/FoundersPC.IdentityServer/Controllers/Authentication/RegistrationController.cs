#region Using namespaces

using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.AuthenticationShared.Request;
using FoundersPC.AuthenticationShared.Response;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [Route("authAPI/registration")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IUserRegistrationService _userRegistrationService;

        public RegistrationController(IAuthenticationService authenticationService,
                                      IMapper mapper,
                                      IUserRegistrationService userRegistrationService
        )
        {
            _authenticationService = authenticationService;
            _mapper = mapper;
            _userRegistrationService = userRegistrationService;
        }

        [HttpPost]
        public async Task<UserRegisterResponse> Register(UserRegisterRequest request)
        {
            if (ReferenceEquals(request, null)) return null;

            var result = await _userRegistrationService.RegisterDefaultUserAsync(request.Email, request.Password);

            if (result)
                return new UserRegisterResponse
                       {
                           Email = request.Email,
                           IsRegistrationSuccessful = true,
                           ResponseException = null
                       };

            return new UserRegisterResponse
                   {
                       Email = request.Email,
                       IsRegistrationSuccessful = false,
                       ResponseException = $"User with email {request.Email} is already exists"
                   };
        }
    }
}
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.AuthenticationShared;
using FoundersPC.Identity.Application.DTO.Request;
using FoundersPC.Identity.Application.DTO.Response;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [Route("authAPI/registration")]
    [ApiController]
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
        public async Task<UserRegisterResponse> TryToRegisterUser(UserRegisterRequestViewModel request)
        {
            if (ReferenceEquals(request, null)) return null;

            var result = await _userService.TryToRegisterUser(request.Email, request.Password);

            if (result)
                return new UserRegisterResponse()
                       {
                           Email = request.Email,
                           IsRegistrationSuccessful = true,
                           ResponseException = null
                       };

            return new UserRegisterResponse()
                   {
                       Email = request.Email,
                       IsRegistrationSuccessful = false,
                       ResponseException = $"User with email {request.Email} is already exists"
                   };
        }
    }
}

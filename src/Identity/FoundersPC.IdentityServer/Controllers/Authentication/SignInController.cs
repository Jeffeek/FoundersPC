using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.AuthenticationShared;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.DTO.Request;
using FoundersPC.Identity.Application.DTO.Response;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using Microsoft.AspNetCore.Mvc;

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [Route("authAPI/login")]
    [ApiController]
    public class SignInController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SignInController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<UserLoginResponse> TryToLoginUser(UserLoginRequestViewModel request)
        {
            if (ReferenceEquals(request, null)) return null;

            var user = await _userService.TryToFindUser(request.LoginOrEmail, request.Password);

            if (ReferenceEquals(user, null)) return new UserLoginResponse();

            var mappedUser = _mapper.Map<UserEntityReadDto, UserLoginResponse>(user);
            mappedUser.IsUserExists = true;

            return mappedUser;
        }
    }
}

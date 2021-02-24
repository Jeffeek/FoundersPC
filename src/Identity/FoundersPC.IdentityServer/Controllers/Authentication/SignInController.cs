#region Using namespaces

using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.AuthenticationShared.Request;
using FoundersPC.AuthenticationShared.Response;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [Route("authAPI/login")]
    [ApiController]
    public class SignInController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public SignInController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<UserLoginResponse> TryToLoginUser(UserLoginRequest request)
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
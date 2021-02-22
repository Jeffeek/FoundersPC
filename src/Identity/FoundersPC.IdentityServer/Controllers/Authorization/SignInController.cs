using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.AuthorizationShared;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services;

namespace FoundersPC.IdentityServer.Controllers.Authorization
{
    [ApiController]
    [Route("AuthorizationAPI/signin")]
    public class SignInController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public SignInController(IUserService userService,
                                IMapper mapper
        )
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<UserAuthorizationResponse> TryToLoginUser(UserAuthorizationRequest request)
        {
            if (ReferenceEquals(request, null)) return null;

            var user = await _userService.TryToFindUser(request.LoginOrEmail, request.RawPassword);

            if (ReferenceEquals(user, null))
                return new UserAuthorizationResponse()
                       {
                           Email = request.LoginOrEmail,
                           IsUserExists = false
                       };

            var mappedUser = _mapper.Map<UserEntityReadDto, UserAuthorizationResponse>(user);

            return new UserAuthorizationResponse()
                   {
                       Email = user.Email,
                       IsUserBlocked = user.IsBlocked,
                       IsUserExists = true
                   };
        }
    }
}

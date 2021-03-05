#region Using namespaces

using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.AuthenticationShared.Request;
using FoundersPC.AuthenticationShared.Response;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.Log_Services;
using FoundersPC.Identity.Application.Interfaces.Services.Mail_service;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.IdentityServer.Controllers.Authentication
{
    [Route("identityAPI/login")]
    [ApiController]
    public class SignInController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IMapper _mapper;
        private readonly IUsersEntrancesService _usersEntrancesService;
        private readonly IUsersService _usersService;

        public SignInController(IUsersService authenticationService,
                                IMapper mapper,
                                IUsersEntrancesService usersEntrancesService,
                                IMailService mailService
        )
        {
            _usersService = authenticationService;
            _mapper = mapper;
            _usersEntrancesService = usersEntrancesService;
            _mailService = mailService;
        }

        [HttpPost]
        public async Task<UserLoginResponse> Login(UserLoginRequest request)
        {
            if (ReferenceEquals(request, null)) return null;

            var user = await _usersService.FindUserByEmailOrLoginAndPasswordAsync(request.LoginOrEmail, request.Password);

            if (ReferenceEquals(user, null)) return new UserLoginResponse();

            await _usersEntrancesService.LogAsync(user.Id);
            if (user.SendMessageOnEntrance)
                await _mailService.SendEntranceNotificationAsync(user.Email);

            var mappedUser = _mapper.Map<UserEntityReadDto, UserLoginResponse>(user);
            mappedUser.IsUserExists = true;

            return mappedUser;
        }
    }
}
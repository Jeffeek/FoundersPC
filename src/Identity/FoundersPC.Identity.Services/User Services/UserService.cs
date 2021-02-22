using System;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services;
using FoundersPC.Identity.Domain.Entities;
using FoundersPC.Identity.Infrastructure.UnitOfWork;

namespace FoundersPC.Identity.Services.User_Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWorkUsersIdentity _unitOfWorkUsersIdentity;
        private readonly IPasswordEncryptorService _encryptorService;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWorkUsersIdentity unitOfWorkUsersIdentity,
                           IPasswordEncryptorService encryptorService,
                           IMapper mapper
        )
        {
            _unitOfWorkUsersIdentity = unitOfWorkUsersIdentity;
            _encryptorService = encryptorService;
            _mapper = mapper;
        }

        public async Task<UserEntityReadDto> TryToFindUser(string emailOrLogin,
                                                           string rawPassword
        )
        {
            if (ReferenceEquals(emailOrLogin, null)) return null;
            if (ReferenceEquals(rawPassword, null)) return null;

            var hashedPassword = _encryptorService.EncryptPassword(rawPassword);

            var user = await _unitOfWorkUsersIdentity.UsersRepository.GetBy(x =>
                                                                                (x.Email == emailOrLogin
                                                                                 || x.Login == emailOrLogin)
                                                                                && x.HashedPassword == hashedPassword);

            return _mapper.Map<UserEntity, UserEntityReadDto>(user);
        }

        public Task<bool> TryToRegisterUser(string email, string rawPassword) => throw new NotImplementedException();
    }
}

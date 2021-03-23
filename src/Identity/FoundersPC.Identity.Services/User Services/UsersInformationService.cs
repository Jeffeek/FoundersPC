#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.DTO;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.Identity.Services.Encryption_Services;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.User_Services
{
    public class UsersInformationService : IUsersInformationService
    {
        private readonly ILogger<UsersInformationService> _logger;
        private readonly IMapper _mapper;
        private readonly PasswordEncryptorService _passwordEncryptorService;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public UsersInformationService(PasswordEncryptorService passwordEncryptorService,
                                       IUnitOfWorkUsersIdentity unitOfWork,
                                       IMapper mapper,
                                       ILogger<UsersInformationService> logger)
        {
            _passwordEncryptorService = passwordEncryptorService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserEntityReadDto>> GetAllUsersAsync() =>
            _mapper.Map<IEnumerable<UserEntity>, IEnumerable<UserEntityReadDto>>(await _unitOfWork.UsersRepository
                .GetAllAsync());

        public async Task<IEnumerable<UserEntityReadDto>> GetAllActiveUsersAsync() =>
            _mapper.Map<IEnumerable<UserEntity>,
                IEnumerable<UserEntityReadDto>>((await _unitOfWork.UsersRepository.GetAllAsync())
                                                .Where(x => x.IsActive));

        public async Task<IEnumerable<UserEntityReadDto>> GetAllNotBlockedUsersAsync() =>
            _mapper.Map<IEnumerable<UserEntity>,
                IEnumerable<UserEntityReadDto>>((await _unitOfWork.UsersRepository.GetAllAsync())
                                                .Where(x => x.IsActive && !x.IsBlocked));

        public async Task<UserEntityReadDto> GetUserByIdAsync(int id)
        {
            if (id < 0) throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 0");

            return _mapper.Map<UserEntity, UserEntityReadDto>(await _unitOfWork.UsersRepository.GetByIdAsync(id));
        }

        public async Task<UserEntityReadDto> FindUserByEmailAsync(string email)
        {
            if (email is null)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: user by email: email was null");

                throw new ArgumentNullException(nameof(email));
            }

            var user = await _unitOfWork.UsersRepository.GetByAsync(x => x.Email == email);

            return user is null ? null : _mapper.Map<UserEntity, UserEntityReadDto>(user);
        }
    }
}
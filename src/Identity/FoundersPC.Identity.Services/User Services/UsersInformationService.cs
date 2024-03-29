﻿#region Using namespaces

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Identity.Application.Interfaces.Services.User_Services;
using FoundersPC.Identity.Domain.Entities.Users;
using FoundersPC.Identity.Dto;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.RequestResponseShared.Pagination.Response;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.User_Services
{
    public class UsersInformationService : IUsersInformationService
    {
        private readonly ILogger<UsersInformationService> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkUsersIdentity _unitOfWorkIdentity;

        public UsersInformationService(IUnitOfWorkUsersIdentity unitOfWorkIdentity,
                                       IMapper mapper,
                                       ILogger<UsersInformationService> logger)
        {
            _unitOfWorkIdentity = unitOfWorkIdentity;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<UserEntityReadDto>> GetAllUsersAsync() =>
            _mapper.Map<IEnumerable<UserEntity>, IEnumerable<UserEntityReadDto>>(await _unitOfWorkIdentity.UsersRepository
                                                                                     .GetAllAsync());

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="predicate"/> is
        ///     <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<IEnumerable<UserEntityReadDto>> GetAllActiveUsersAsync() =>
            _mapper.Map<IEnumerable<UserEntity>,
                IEnumerable<UserEntityReadDto>>((await _unitOfWorkIdentity.UsersRepository.GetAllAsync())
                                                .Where(x => x.IsActive));

        #region Docs

        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="source"/> or <paramref name="predicate"/> is
        ///     <see langword="null"/>.
        /// </exception>

        #endregion

        public async Task<IEnumerable<UserEntityReadDto>> GetAllNotBlockedUsersAsync() =>
            _mapper.Map<IEnumerable<UserEntity>,
                IEnumerable<UserEntityReadDto>>((await _unitOfWorkIdentity.UsersRepository.GetAllAsync())
                                                .Where(x => x.IsActive && !x.IsBlocked));

        #region Docs

        /// <exception cref="T:System.ArgumentOutOfRangeException">id &lt; 1.</exception>

        #endregion

        public async Task<UserEntityReadDto> GetUserByIdAsync(int id)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(id), "Id must be greater than 1");

            return _mapper.Map<UserEntity, UserEntityReadDto>(await _unitOfWorkIdentity.UsersRepository.GetByIdAsync(id));
        }

        #region Docs

        /// <exception cref="T:System.ArgumentNullException"><paramref name="email"/> is <see langword="null"/></exception>

        #endregion

        public async Task<UserEntityReadDto> FindUserByEmailAsync(string email)
        {
            if (email is null)
            {
                _logger.LogError($"{nameof(UsersInformationService)}: user by email: email was null");

                throw new ArgumentNullException(nameof(email));
            }

            var user = await _unitOfWorkIdentity.UsersRepository.GetUserByEmailAsync(email);

            return user is null ? null : _mapper.Map<UserEntity, UserEntityReadDto>(user);
        }

        #region Docs

        /// <inheritdoc/>

        #endregion

        public async Task<IPaginationResponse<UserEntityReadDto>> GetPaginateableAsync(int pageNumber = 1,
                                                                                       int pageSize = 10)
        {
            var items = _mapper.Map<IEnumerable<UserEntity>,
                IEnumerable<UserEntityReadDto>>(await _unitOfWorkIdentity.UsersRepository
                                                                         .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWorkIdentity.UsersRepository.CountAsync();

            return new PaginationResponse<UserEntityReadDto>(items, totalItemsCount);
        }
    }
}
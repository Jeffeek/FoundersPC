#region Using namespaces

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Dto;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.RequestResponseShared.Request.Tokens;
using FoundersPC.RequestResponseShared.Response.Pagination;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.Token_Services
{
    public class AccessUsersTokensService : IAccessUsersTokensService
    {
        private readonly ILogger<AccessUsersTokensService> _logger;
        private readonly IMapper _mapper;
        private readonly IAccessTokensReservationService _reservationService;
        private readonly IAccessTokensBlockingService _blockingService;
        private readonly IAccessTokensRequestsService _requestsService;
        private readonly IAccessTokensTokensStatusService _statusService;
        private readonly IUnitOfWorkUsersIdentity _unitOfWork;

        public AccessUsersTokensService(IUnitOfWorkUsersIdentity unitOfWork,
                                        IAccessTokensReservationService reservationService,
                                        IAccessTokensBlockingService blockingService,
                                        IAccessTokensRequestsService requestsService,
                                        IAccessTokensTokensStatusService statusService,
                                        IMapper mapper,
                                        ILogger<AccessUsersTokensService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _reservationService = reservationService;
            _blockingService = blockingService;
            _requestsService = requestsService;
            _statusService = statusService;
            _logger = logger;
        }

        public async Task<IEnumerable<AccessUserTokenReadDto>> GetUserTokensAsync(int userId)
        {
            var tokens = await _unitOfWork.ApiAccessUsersTokensRepository.GetAllUserTokens(userId);

            if (tokens is null)
                return null;

            return _mapper.Map<IEnumerable<ApiAccessUserToken>,
                IEnumerable<AccessUserTokenReadDto>>(tokens);
        }

        public async Task<IEnumerable<AccessUserTokenReadDto>> GetUserTokensAsync(string userEmail)
        {
            var tokens = await _unitOfWork.ApiAccessUsersTokensRepository.GetAllUserTokens(userEmail);

            if (tokens is null)
                return null;

            return _mapper.Map<IEnumerable<ApiAccessUserToken>,
                IEnumerable<AccessUserTokenReadDto>>(tokens);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<AccessUserTokenReadDto>> GetAllTokensAsync() =>
            _mapper.Map<IEnumerable<ApiAccessUserToken>,
                IEnumerable<AccessUserTokenReadDto>>(await _unitOfWork.ApiAccessUsersTokensRepository
                                                                      .GetAllAsync());

        #region Implementation of IAccessTokensStatusService

        #region IsTokenBlocked

        public async Task<bool> IsTokenBlockedAsync(string token) => await _statusService.IsTokenBlockedAsync(token);

        public async Task<bool> IsTokenBlockedAsync(int id) => await _statusService.IsTokenBlockedAsync(id);

        #endregion

        #region IsTokenActive

        public async Task<bool> IsTokenActiveAsync(string token) => await _statusService.IsTokenActiveAsync(token);

        public async Task<bool> IsTokenActiveAsync(int id) => await _statusService.IsTokenActiveAsync(id);

        #endregion

        #endregion

        #region Implementation of IAccessTokensRequestsService

        public async Task<bool> CanMakeRequestAsync(string token) => await _requestsService.CanMakeRequestAsync(token);

        public async Task<bool> CanMakeRequestAsync(int tokenId) => await _requestsService.CanMakeRequestAsync(tokenId);

        #endregion

        #region Implementation of IAccessTokensBlockingService

        public async Task<bool> BlockAsync(string token) => await _blockingService.BlockAsync(token);

        public async Task<bool> BlockAsync(int id) => await _blockingService.BlockAsync(id);

        #endregion

        #region Implementation of IAccessTokensReservationService

        /// <inheritdoc />
        public async Task<AccessUserTokenReadDto> ReserveNewTokenAsync(string userEmail, TokenType type) =>
            await _reservationService.ReserveNewTokenAsync(userEmail, type);

        /// <inheritdoc />
        public async Task<AccessUserTokenReadDto> ReserveNewTokenAsync(int userId, TokenType type) =>
            await _reservationService.ReserveNewTokenAsync(userId, type);

        #endregion

        #region Implementation of IPaginateableService<AccessUserTokenReadDto>

        /// <inheritdoc />
        public async Task<IPaginationResponse<AccessUserTokenReadDto>> GetPaginateableAsync(int pageNumber = 1,
                                                                                            int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<ApiAccessUserToken>,
                IEnumerable<AccessUserTokenReadDto>>(await _unitOfWork.ApiAccessUsersTokensRepository
                                                                      .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWork.ApiAccessUsersTokensRepository.CountAsync();

            return new PaginationResponse<AccessUserTokenReadDto>(items, totalItemsCount);
        }

        #endregion
    }
}
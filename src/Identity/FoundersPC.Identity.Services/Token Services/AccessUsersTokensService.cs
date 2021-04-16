#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Identity.Application.Interfaces.Services.Token_Services;
using FoundersPC.Identity.Domain.Entities.Tokens;
using FoundersPC.Identity.Dto;
using FoundersPC.Identity.Infrastructure.UnitOfWork;
using FoundersPC.RequestResponseShared.IdentityServer.Request.Tokens;
using FoundersPC.RequestResponseShared.Pagination;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Identity.Services.Token_Services
{
    public class AccessUsersTokensService : IAccessUsersTokensService
    {
        private readonly IAccessTokensBlockingService _blockingService;
        private readonly ILogger<AccessUsersTokensService> _logger;
        private readonly IMapper _mapper;
        private readonly IAccessTokensRequestsService _requestsService;
        private readonly IAccessTokensReservationService _reservationService;
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

        /// <inheritdoc/>
        public async Task<IEnumerable<AccessUserTokenReadDto>> GetAllTokensAsync() =>
            _mapper.Map<IEnumerable<ApiAccessUserToken>,
                IEnumerable<AccessUserTokenReadDto>>(await _unitOfWork.ApiAccessUsersTokensRepository
                                                                      .GetAllAsync());

        #region Implementation of IPaginateableService<AccessUserTokenReadDto>

        /// <inheritdoc/>
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

        #region Implementation of IAccessTokensStatusService

        #region IsTokenBlocked

        public Task<bool> IsTokenBlockedAsync(string token) => _statusService.IsTokenBlockedAsync(token);

        public Task<bool> IsTokenBlockedAsync(int id) => _statusService.IsTokenBlockedAsync(id);

        #endregion

        #region IsTokenActive

        public Task<bool> IsTokenActiveAsync(string token) => _statusService.IsTokenActiveAsync(token);

        public Task<bool> IsTokenActiveAsync(int id) => _statusService.IsTokenActiveAsync(id);

        #endregion

        #endregion

        #region Implementation of IAccessTokensRequestsService

        public Task<bool> CanMakeRequestAsync(string token) => _requestsService.CanMakeRequestAsync(token);

        public Task<bool> CanMakeRequestAsync(int tokenId) => _requestsService.CanMakeRequestAsync(tokenId);

        #endregion

        #region Implementation of IAccessTokensBlockingService

        public Task<bool> BlockAsync(string token) => _blockingService.BlockAsync(token);

        public Task<bool> BlockAsync(int id) => _blockingService.BlockAsync(id);

        #endregion

        #region Implementation of IAccessTokensReservationService

        /// <inheritdoc/>
        public Task<AccessUserTokenReadDto> ReserveNewTokenAsync(string userEmail, TokenType type) =>
            _reservationService.ReserveNewTokenAsync(userEmail, type);

        /// <inheritdoc/>
        public Task<AccessUserTokenReadDto> ReserveNewTokenAsync(int userId, TokenType type) =>
            _reservationService.ReserveNewTokenAsync(userId, type);

        #endregion
    }
}
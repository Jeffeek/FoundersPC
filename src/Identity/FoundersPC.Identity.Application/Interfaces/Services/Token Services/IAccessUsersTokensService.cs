#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    public interface IAccessUsersTokensService : IPaginateableService<AccessTokenReadDto>,
                                                 IAccessTokensBlockingService,
                                                 IAccessTokensTokensStatusService,
                                                 IAccessTokensRequestsService,
                                                 IAccessTokensReservationService
    {
        Task<IEnumerable<AccessTokenReadDto>> GetAllTokensAsync();

        Task<IEnumerable<AccessTokenReadDto>> GetUserTokensAsync(int userId);

        Task<IEnumerable<AccessTokenReadDto>> GetUserTokensAsync(string userEmail);
    }
}
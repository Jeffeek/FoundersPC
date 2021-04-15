#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Identity.Dto;
using FoundersPC.RepositoryShared.Repository;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.Identity.Application.Interfaces.Services.Token_Services
{
    public interface IAccessUsersTokensService : IPaginateableService<AccessUserTokenReadDto>,
                                                 IAccessTokensBlockingService,
                                                 IAccessTokensTokensStatusService,
                                                 IAccessTokensRequestsService,
                                                 IAccessTokensReservationService
    {
        Task<IEnumerable<AccessUserTokenReadDto>> GetAllTokensAsync();

        Task<IEnumerable<AccessUserTokenReadDto>> GetUserTokensAsync(int userId);

        Task<IEnumerable<AccessUserTokenReadDto>> GetUserTokensAsync(string userEmail);
    }
}
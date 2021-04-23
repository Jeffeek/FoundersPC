#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
    public interface IVideoCardCoresManagingService
    {
        Task<IEnumerable<VideoCardCoreReadDto>> GetAllVideoCardCoresAsync(string managerToken);

        Task<VideoCardCoreReadDto> GetVideoCardCoreByIdAsync(int id, string managerToken);

        Task<bool> UpdateVideoCardCoreAsync(int id, VideoCardCoreUpdateDto videoCardCore, string managerToken);

        Task<bool> DeleteVideoCardCoreAsync(int videoCardCoreId, string managerToken);

        Task<bool> CreateVideoCardCoreAsync(VideoCardCoreInsertDto videoCardCore, string managerToken);

        Task<IPaginationResponse<VideoCardCoreReadDto>> GetPaginateableVideoCardCoresAsync(int pageNumber,
                                                                                           int pageSize,
                                                                                           string managerToken);
    }
}
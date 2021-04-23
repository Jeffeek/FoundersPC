#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
    public interface IVideoCardsManagingService
    {
        Task<IEnumerable<VideoCardReadDto>> GetAllVideoCardsAsync(string managerToken);

        Task<VideoCardReadDto> GetVideoCardByIdAsync(int id, string managerToken);

        Task<bool> UpdateVideoCardAsync(int id, VideoCardUpdateDto videoCard, string managerToken);

        Task<bool> DeleteVideoCardAsync(int videoCardId, string managerToken);

        Task<bool> CreateVideoCardAsync(VideoCardInsertDto videoCard, string managerToken);

        Task<IPaginationResponse<VideoCardReadDto>> GetPaginateableVideoCardsAsync(int pageNumber,
                                                                                   int pageSize,
                                                                                   string managerToken);
    }
}
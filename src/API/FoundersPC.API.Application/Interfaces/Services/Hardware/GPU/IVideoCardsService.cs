#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.GPU
{
    public interface IVideoCardsService : IPaginateableService<VideoCardReadDto>
    {
        Task<IEnumerable<VideoCardReadDto>> GetAllVideoCardsAsync();

        Task<VideoCardReadDto> GetVideoCardByIdAsync(int gpuId);

        Task<bool> CreateVideoCardAsync(VideoCardInsertDto videoCard);

        Task<bool> UpdateVideoCardAsync(int id, VideoCardUpdateDto videoCard);

        Task<bool> DeleteVideoCardAsync(int id);
    }
}
#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.GPU
{
    public interface IVideoCardCoresService : IPaginateableService<VideoCardCoreReadDto>
    {
        Task<IEnumerable<VideoCardCoreReadDto>> GetAllVideoCardCoresAsync();

        Task<VideoCardCoreReadDto> GetVideoCardCoreByIdAsync(int videoCardCoreId);

        Task<bool> CreateVideoCardCoreAsync(VideoCardCoreInsertDto videoCardCore);

        Task<bool> UpdateVideoCardCoreAsync(int id, VideoCardCoreUpdateDto videoCardCore);

        Task<bool> DeleteVideoCardCoreAsync(int id);
    }
}
#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.GPU
{
    public interface IVideoCardCoreService
    {
        Task<IEnumerable<VideoCardCoreReadDto>> GetAllVideoCardCoresAsync();

        Task<VideoCardCoreReadDto> GetVideoCardCoreByIdAsync(int videoCardCoreId);

        Task<bool> CreateVideoCardCoreAsync(VideoCardCoreInsertDto videoCardCore);

        Task<bool> UpdateVideoCardCoreAsync(int id, VideoCardCoreUpdateDto videoCardCore);

        Task<bool> DeleteVideoCardCoreAsync(int id);
    }
}
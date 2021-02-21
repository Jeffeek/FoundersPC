#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.GPU
{
    public interface IVideoCardCoreService
    {
        Task<IEnumerable<VideoCardCoreReadDto>> GetAllVideoCardCoresAsync();

        Task<VideoCardCoreReadDto> GetVideoCardCoreByIdAsync(int videoCardCoreId);

        Task<bool> CreateVideoCardCore(VideoCardCoreInsertDto videoCardCore);

        Task<bool> UpdateVideoCardCore(int id, VideoCardCoreUpdateDto videoCardCore);

        Task<bool> DeleteVideoCardCore(int id);
    }
}
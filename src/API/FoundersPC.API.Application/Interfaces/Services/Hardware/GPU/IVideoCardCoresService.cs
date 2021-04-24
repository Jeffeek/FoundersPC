#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware.GPU
{
    /// <summary>
    ///     Interface for decoration of database logic with entities
    /// </summary>
    public interface IVideoCardCoresService : IPaginateableService<VideoCardCoreReadDto>
    {
        /// <summary>
        ///     Return an enumeration of all <see cref="VideoCardCoreReadDto"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<VideoCardCoreReadDto>> GetAllVideoCardCoresAsync();

        Task<VideoCardCoreReadDto> GetVideoCardCoreByIdAsync(int videoCardCoreId);

        Task<bool> CreateVideoCardCoreAsync(VideoCardCoreInsertDto videoCardCore);

        Task<bool> UpdateVideoCardCoreAsync(int id, VideoCardCoreUpdateDto videoCardCore);

        Task<bool> DeleteVideoCardCoreAsync(int id);
    }
}
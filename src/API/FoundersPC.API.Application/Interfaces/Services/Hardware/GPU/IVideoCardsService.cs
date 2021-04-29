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
    public interface IVideoCardsService : IPaginateableService<VideoCardReadDto>
    {
        /// <summary>
        ///     Return an enumeration of all <see cref="VideoCardReadDto"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<VideoCardReadDto>> GetAllVideoCardsAsync();

        Task<VideoCardReadDto> GetVideoCardByIdAsync(int gpuId);

        Task<bool> CreateVideoCardAsync(VideoCardInsertDto videoCard);

        Task<bool> UpdateVideoCardAsync(int id, VideoCardUpdateDto videoCard);

        Task<bool> DeleteVideoCardAsync(int id);
    }
}
#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware.VideoCard;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.VideoCard
{
    /// <summary>
    ///     Interface for <see cref="VideoCardCore"/> database access
    /// </summary>
    public interface IVideoCardCoresRepositoryAsync : IRepositoryAsync<VideoCardCore>,
                                                      IPaginateableRepository<VideoCardCore> { }
}
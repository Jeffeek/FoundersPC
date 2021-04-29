#region Using namespaces

using FoundersPC.API.Domain.Entities.VideoCard;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.VideoCard
{
    /// <summary>
    ///     Interface for <see cref="VideoCardEntity"/> database access
    /// </summary>
    public interface IVideoCardsRepositoryAsync : IRepositoryAsync<VideoCardEntity>,
                                                  IPaginateableRepository<VideoCardEntity> { }
}
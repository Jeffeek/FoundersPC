#region Using namespaces

using FoundersPC.API.Domain.Entities.VideoCard;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.VideoCard
{
    public interface IVideoCardCoresRepositoryAsync : IRepositoryAsync<VideoCardCoreEntity>,
                                                      IPaginateableRepository<VideoCardCoreEntity> { }
}
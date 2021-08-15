#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.VideoCard
{
    /// <summary>
    ///     Interface for <see cref="VideoCard"/> database access
    /// </summary>
    public interface IVideoCardsRepositoryAsync : IRepositoryAsync<Domain.Entities.Hardware.VideoCard>,
                                                  IPaginateableRepository<Domain.Entities.Hardware.VideoCard> { }
}
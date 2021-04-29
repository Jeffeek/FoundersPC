#region Using namespaces

using FoundersPC.API.Domain.Entities.Memory;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Memory
{
    /// <summary>
    ///     Interface for <see cref="RandomAccessMemoryEntity"/> database access
    /// </summary>
    public interface IRandomAccessMemoryRepositoryAsync : IRepositoryAsync<RandomAccessMemoryEntity>,
                                                          IPaginateableRepository<RandomAccessMemoryEntity> { }
}
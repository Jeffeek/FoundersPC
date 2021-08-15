#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Memory
{
    /// <summary>
    ///     Interface for <see cref="RandomAccessMemory"/> database access
    /// </summary>
    public interface IRandomAccessMemoryRepositoryAsync : IRepositoryAsync<RandomAccessMemory>,
                                                          IPaginateableRepository<RandomAccessMemory> { }
}
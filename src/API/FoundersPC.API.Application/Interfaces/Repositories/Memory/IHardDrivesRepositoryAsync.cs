#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Memory
{
    /// <inheritdoc/>
    /// <summary>
    ///     Interface for <see cref="T:FoundersPC.API.Domain.Entities.Memory.HardDriveDiskEntity"/> database access
    /// </summary>
    public interface IHardDrivesRepositoryAsync : IRepositoryAsync<HardDriveDisk>,
                                                  IPaginateableRepository<HardDriveDisk> { }
}
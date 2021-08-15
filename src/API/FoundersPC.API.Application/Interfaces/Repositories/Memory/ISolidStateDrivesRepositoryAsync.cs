#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Memory
{
    /// <summary>
    ///     Interface for <see cref="SolidStateDrive"/> database access
    /// </summary>
    public interface ISolidStateDrivesRepositoryAsync : IRepositoryAsync<SolidStateDrive>,
                                                        IPaginateableRepository<SolidStateDrive> { }
}
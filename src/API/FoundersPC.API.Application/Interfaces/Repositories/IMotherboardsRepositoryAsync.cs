#region Using namespaces

using FoundersPC.API.Domain.Entities;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories
{
    /// <summary>
    ///     Interface for <see cref="Motherboard"/> database access
    /// </summary>
    public interface
        IMotherboardsRepositoryAsync : IRepositoryAsync<Motherboard>,
                                       IPaginateableRepository<Motherboard> { }
}
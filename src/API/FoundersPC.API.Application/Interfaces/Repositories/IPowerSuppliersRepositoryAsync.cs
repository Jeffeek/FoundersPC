#region Using namespaces

using FoundersPC.API.Domain.Entities;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories
{
    /// <summary>
    ///     Interface for <see cref="PowerSupply"/> database access
    /// </summary>
    public interface IPowerSuppliersRepositoryAsync : IRepositoryAsync<PowerSupply>,
                                                      IPaginateableRepository<PowerSupply> { }
}
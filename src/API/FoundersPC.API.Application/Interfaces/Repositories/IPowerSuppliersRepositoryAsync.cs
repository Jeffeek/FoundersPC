#region Using namespaces

using FoundersPC.API.Domain.Entities;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories
{
    /// <summary>
    ///     Interface for <see cref="PowerSupplyEntity"/> database access
    /// </summary>
    public interface IPowerSuppliersRepositoryAsync : IRepositoryAsync<PowerSupplyEntity>,
                                                      IPaginateableRepository<PowerSupplyEntity> { }
}
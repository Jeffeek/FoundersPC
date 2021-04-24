#region Using namespaces

using FoundersPC.API.Domain.Entities;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories
{
    /// <summary>
    ///     Interface for <see cref="MotherboardEntity"/> database access
    /// </summary>
    public interface
        IMotherboardsRepositoryAsync : IRepositoryAsync<MotherboardEntity>,
                                       IPaginateableRepository<MotherboardEntity> { }
}
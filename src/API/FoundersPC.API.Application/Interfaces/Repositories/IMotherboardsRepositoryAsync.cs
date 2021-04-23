#region Using namespaces

using FoundersPC.API.Domain.Entities;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories
{
    public interface
        IMotherboardsRepositoryAsync : IRepositoryAsync<MotherboardEntity>,
                                       IPaginateableRepository<MotherboardEntity> { }
}
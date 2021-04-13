#region Using namespaces

using FoundersPC.API.Domain.Entities.Memory;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Memory
{
    public interface IHDDsRepositoryAsync : IRepositoryAsync<HDD>,
                                            IPaginateableRepository<HDD> { }
}
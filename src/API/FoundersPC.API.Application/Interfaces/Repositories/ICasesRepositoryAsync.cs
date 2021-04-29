#region Using namespaces

using FoundersPC.API.Domain.Entities;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories

{
    /// <summary>
    ///     Interface for <see cref="CaseEntity"/> database access
    /// </summary>
    public interface ICasesRepositoryAsync : IRepositoryAsync<CaseEntity>,
                                             IPaginateableRepository<CaseEntity> { }
}
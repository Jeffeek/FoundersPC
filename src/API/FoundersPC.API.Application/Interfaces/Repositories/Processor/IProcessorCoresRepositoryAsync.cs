#region Using namespaces

using FoundersPC.API.Domain.Entities.Processor;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Processor
{
    /// <summary>
    ///     Interface for <see cref="ProcessorCoreEntity"/> database access
    /// </summary>
    public interface IProcessorCoresRepositoryAsync : IRepositoryAsync<ProcessorCoreEntity>,
                                                      IPaginateableRepository<ProcessorCoreEntity> { }
}
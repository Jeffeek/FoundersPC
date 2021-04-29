#region Using namespaces

using FoundersPC.API.Domain.Entities.Processor;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Processor
{
    /// <summary>
    ///     Interface for <see cref="ProcessorEntity"/> database access
    /// </summary>
    public interface IProcessorsRepositoryAsync : IRepositoryAsync<ProcessorEntity>,
                                                  IPaginateableRepository<ProcessorEntity> { }
}
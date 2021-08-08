#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware.Processor;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Processor
{
    /// <summary>
    ///     Interface for <see cref="ProcessorCore"/> database access
    /// </summary>
    public interface IProcessorCoresRepositoryAsync : IRepositoryAsync<ProcessorCore>,
                                                      IPaginateableRepository<ProcessorCore> { }
}
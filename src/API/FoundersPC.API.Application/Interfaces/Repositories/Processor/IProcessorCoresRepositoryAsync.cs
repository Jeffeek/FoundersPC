#region Using namespaces

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
#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Processor
{
    /// <summary>
    ///     Interface for <see cref="Processor"/> database access
    /// </summary>
    public interface IProcessorsRepositoryAsync : IRepositoryAsync<Domain.Entities.Hardware.Processor>,
                                                  IPaginateableRepository<Domain.Entities.Hardware.Processor> { }
}
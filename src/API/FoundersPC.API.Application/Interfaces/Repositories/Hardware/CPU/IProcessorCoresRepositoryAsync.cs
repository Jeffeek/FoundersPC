#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware.Processor;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Hardware.CPU
{
    public interface IProcessorCoresRepositoryAsync : IRepositoryAsync<ProcessorCore> { }
}
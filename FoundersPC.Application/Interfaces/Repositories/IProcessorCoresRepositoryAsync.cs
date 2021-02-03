#region Using derectives

using FoundersPC.Domain.Entities.Hardware.Processor;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories
{
	public interface IProcessorCoresRepositoryAsync : IGenericRepositoryAsync<ProcessorCore> { }
}
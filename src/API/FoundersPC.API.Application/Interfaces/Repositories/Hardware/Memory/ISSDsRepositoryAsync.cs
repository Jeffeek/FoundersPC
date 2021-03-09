#region Using namespaces

using FoundersPC.API.Domain.Entities.Hardware.Memory;
using FoundersPC.RepositoryShared.Repository;

#endregion

namespace FoundersPC.API.Application.Interfaces.Repositories.Hardware.Memory
{
	public interface ISSDsRepositoryAsync : IRepositoryAsync<SSD> { }
}
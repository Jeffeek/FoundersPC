#region Using derectives

using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public class ProducersRepository : GenericRepositoryAsync<Producer>, IProducersRepositoryAsync
	{
		/// <inheritdoc />
		public ProducersRepository(DbContext repositoryContext) : base(repositoryContext) { }
	}
}
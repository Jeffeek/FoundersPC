#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Domain.Entities.Hardware.Memory;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public class RAMsRepository : GenericRepositoryAsync<RAM>, IRAMsRepositoryAsync
	{
		/// <inheritdoc />
		public RAMsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Overrides of GenericRepositoryAsync<Case>

		/// <inheritdoc />
		public override async Task<IQueryable<RAM>> GetAllAsync() =>
			(await base.GetAllAsync()).Include(ram => ram.Producer);

		#endregion
	}
}
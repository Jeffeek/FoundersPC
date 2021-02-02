#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Domain.Entities.Hardware.Memory;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public class HDDsRepository : GenericRepositoryAsync<HDD>, IHDDsRepositoryAsync
	{
		/// <inheritdoc />
		public HDDsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Overrides of GenericRepositoryAsync<Case>

		/// <inheritdoc />
		public override async Task<IQueryable<HDD>> GetAllAsync() =>
			(await base.GetAllAsync()).Include(hdd => hdd.Producer);

		#endregion
	}
}
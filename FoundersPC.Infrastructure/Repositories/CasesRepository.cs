#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Application.Interfaces.Repositories;
using FoundersPC.Domain.Entities.Hardware;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Infrastructure.Repositories
{
	public class CasesRepository : GenericRepositoryAsync<Case>, ICasesRepositoryAsync
	{
		/// <inheritdoc />
		public CasesRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Overrides of GenericRepositoryAsync<Case>

		/// <inheritdoc />
		public override async Task<IQueryable<Case>> GetAllAsync() =>
			(await base.GetAllAsync()).Include(@case => @case.Producer);

		#endregion
	}
}
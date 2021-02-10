#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.Case
{
	public class CasesRepository : RepositoryBase<Models.Hardware.Case>, ICasesRepository
	{
		/// <inheritdoc />
		public CasesRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of ICasesRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.Case>> GetAllCasesAsync() => await GetAll()
			.Include(x => x.Producer)
			.ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.Case> GetCaseByIdAsync(int caseId) =>
			await FindBy(@case => @case.Id == caseId)
			      .Include(x => x.Producer)
			      .FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreateCase(Models.Hardware.Case @case) => await Task.Run(() => Create(@case));
		
		/// <inheritdoc />
		public async Task UpdateCase(Models.Hardware.Case @case) => await Task.Run(() => Update(@case));
		
		/// <inheritdoc />
		public async Task DeleteCase(Models.Hardware.Case @case) => await Task.Run(() => Delete(@case));
		

		#endregion
	}
}
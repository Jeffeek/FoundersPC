#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;

#endregion

namespace FoundersPC.Services.Repositories.Case
{
	public interface ICasesRepository : IRepository<Models.Hardware.Case>
	{
		Task<IEnumerable<Models.Hardware.Case>> GetAllCasesAsync();
		Task<Models.Hardware.Case> GetCaseByIdAsync(int caseId);
		Task CreateCase(Models.Hardware.Case @case);
		Task UpdateCase(Models.Hardware.Case @case);
		Task DeleteCase(Models.Hardware.Case @case);
	}
}
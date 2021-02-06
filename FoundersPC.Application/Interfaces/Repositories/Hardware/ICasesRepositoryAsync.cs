#region Using derectives

using System.Linq;
using System.Threading.Tasks;
using FoundersPC.Domain.Entities.Hardware;

#endregion

namespace FoundersPC.Application.Interfaces.Repositories.Hardware

{
	public interface ICasesRepositoryAsync
	{
		Task<Case> AddAsync(Case @case);

		Task UpdateAsync(Case @case);

		Task DeleteAsync(Case @case);

		Task<Case> GetByIdAsync(int id);

		Task<IQueryable<Case>> GetAllAsync();
	}
}
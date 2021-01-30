#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Services.Repositories.Base;
using Microsoft.EntityFrameworkCore;

#endregion

namespace FoundersPC.Services.Repositories.Motherboard
{
	public class MotherboardsRepository : RepositoryBase<Models.Hardware.Motherboard>, IMotherboardsRepository
	{
		/// <inheritdoc />
		public MotherboardsRepository(DbContext repositoryContext) : base(repositoryContext) { }

		#region Implementation of IMotherboardsRepository

		/// <inheritdoc />
		public async Task<IEnumerable<Models.Hardware.Motherboard>> GetAllMotherboardsAsync() => await GetAll()
			.Include(x => x.Producer)
			.ToListAsync();

		/// <inheritdoc />
		public async Task<Models.Hardware.Motherboard> GetMotherboardByIdAsync(int motherboardsId) =>
			await FindBy(motherboard => motherboard.Id == motherboardsId)
			      .Include(motherboard => motherboard.Producer)
			      .FirstOrDefaultAsync();

		/// <inheritdoc />
		public async Task CreateMotherboard(Models.Hardware.Motherboard motherboard) => await Task.Run(() => Create(motherboard));
		
		/// <inheritdoc />
		public async Task UpdateMotherboard(Models.Hardware.Motherboard motherboard) => await Task.Run(() => Update(motherboard));
		
		/// <inheritdoc />
		public async Task DeleteMotherboard(Models.Hardware.Motherboard motherboard) => await Task.Run(() => Delete(motherboard));
		

		#endregion
	}
}
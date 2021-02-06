#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Application.Interfaces.Services.Hardware
{
	public interface IMotherboardService
	{
		Task<IEnumerable<MotherboardReadDto>> GetAllMotherboardsAsync();
		Task<MotherboardReadDto> GetMotherboardByIdAsync(int motherboard);
		Task<bool> CreateMotherboard(MotherboardInsertDto motherboard);
		Task<bool> UpdateMotherboard(int id, MotherboardUpdateDto motherboard);
		Task<bool> DeleteMotherboard(int id);
	}
}
#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware
{
	public interface IMotherboardService
	{
		Task<IEnumerable<MotherboardReadDto>> GetAllMotherboardsAsync();

		Task<MotherboardReadDto> GetMotherboardByIdAsync(int motherboardId);

		Task<bool> CreateMotherboardAsync(MotherboardInsertDto motherboard);

		Task<bool> UpdateMotherboardAsync(int id, MotherboardUpdateDto motherboard);

		Task<bool> DeleteMotherboardAsync(int id);
	}
}
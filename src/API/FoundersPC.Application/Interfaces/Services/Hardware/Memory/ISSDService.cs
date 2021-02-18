#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Application.Interfaces.Services.Hardware.Memory
{
	public interface ISSDService
	{
		Task<IEnumerable<SSDReadDto>> GetAllSSDsAsync();

		Task<SSDReadDto> GetSSDByIdAsync(int ssdId);

		Task<bool> CreateSSD(SSDInsertDto ssd);

		Task<bool> UpdateSSD(int id, SSDUpdateDto ssd);

		Task<bool> DeleteSSD(int id);
	}
}
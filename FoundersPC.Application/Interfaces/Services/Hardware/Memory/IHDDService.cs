#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Application.Interfaces.Services.Hardware.Memory
{
	public interface IHDDService
	{
		Task<IEnumerable<HDDReadDto>> GetAllHDDsAsync();
		Task<HDDReadDto> GetHDDByIdAsync(int hdd);
		Task<bool> CreateHDD(HDDInsertDto hdd);
		Task<bool> UpdateHDD(int id, HDDUpdateDto hdd);
		Task<bool> DeleteHDD(int id);
	}
}
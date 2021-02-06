using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.Memory;

namespace FoundersPC.Services.Hardware_Services.Hardware.Memory
{
	public class RAMService : IRAMService
	{
		#region Implementation of IRAMService

		/// <inheritdoc />
		public Task<IEnumerable<RAMReadDto>> GetAllRAMsAsync() => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<RAMReadDto> GetRAMByIdAsync(int ram) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> CreateRAM(RAMInsertDto ram) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> UpdateRAM(int id, RAMUpdateDto ram) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> DeleteRAM(int id) => throw new NotImplementedException();

		#endregion
	}
}

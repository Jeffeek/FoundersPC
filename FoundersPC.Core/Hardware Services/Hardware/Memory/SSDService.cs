using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.Memory;

namespace FoundersPC.Services.Hardware_Services.Hardware.Memory
{
	public class SSDService : ISSDService
	{
		#region Implementation of ISSDService

		/// <inheritdoc />
		public Task<IEnumerable<SSDReadDto>> GetAllSSDsAsync() => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<SSDReadDto> GetSSDByIdAsync(int ssd) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> CreateSSD(SSDInsertDto ssd) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> UpdateSSD(int id, SSDUpdateDto ssd) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> DeleteSSD(int id) => throw new NotImplementedException();

		#endregion
	}
}

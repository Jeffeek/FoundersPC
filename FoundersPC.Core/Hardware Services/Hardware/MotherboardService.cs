using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware;

namespace FoundersPC.Services.Hardware_Services.Hardware
{
	public class MotherboardService : IMotherboardService
	{
		#region Implementation of IMotherboardService

		/// <inheritdoc />
		public Task<IEnumerable<MotherboardReadDto>> GetAllMotherboardsAsync() => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<MotherboardReadDto> GetMotherboardByIdAsync(int motherboard) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> CreateMotherboard(MotherboardInsertDto motherboard) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> UpdateMotherboard(int id, MotherboardUpdateDto motherboard) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> DeleteMotherboard(int id) => throw new NotImplementedException();

		#endregion
	}
}

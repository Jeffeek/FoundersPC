using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.Memory;

namespace FoundersPC.Services.Hardware_Services.Hardware.Memory
{
	public class HDDService : IHDDService
	{
		#region Implementation of IHDDService

		/// <inheritdoc />
		public Task<IEnumerable<HDDReadDto>> GetAllHDDsAsync() => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<HDDReadDto> GetHDDByIdAsync(int hdd) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> CreateHDD(HDDInsertDto hdd) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> UpdateHDD(int id, HDDUpdateDto hdd) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> DeleteHDD(int id) => throw new NotImplementedException();

		#endregion
	}
}

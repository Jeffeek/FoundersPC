using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.GPU;

namespace FoundersPC.Services.Hardware_Services.Hardware.GPU
{
	public class GPUService : IGPUService
	{
		#region Implementation of IGPUService

		/// <inheritdoc />
		public Task<IEnumerable<GPUReadDto>> GetAllGPUsAsync() => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<GPUReadDto> GetGPUByIdAsync(int gpuId) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> CreateGPU(GPUInsertDto gpu) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> UpdateGPU(int id, GPUUpdateDto gpu) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> DeleteGPU(int id) => throw new NotImplementedException();

		#endregion
	}
}

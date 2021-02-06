using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware;

namespace FoundersPC.Services.Hardware_Services.Hardware
{
	public class PowerSupplyService : IPowerSupplyService
	{
		#region Implementation of IPowerSupplyService

		/// <inheritdoc />
		public Task<IEnumerable<PowerSupplyReadDto>> GetAllPowerSuppliersAsync() => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<PowerSupplyReadDto> GetSSDByIdAsync(int powerSupply) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> CreatePowerSupply(PowerSupplyInsertDto powerSupply) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> UpdatePowerSupply(int id, PowerSupplyUpdateDto powerSupply) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> DeletePowerSupply(int id) => throw new NotImplementedException();

		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware;

namespace FoundersPC.Services.Hardware_Services.Hardware
{
	public class CaseService : ICaseService
	{
		#region Implementation of ICaseService

		/// <inheritdoc />
		public Task<IEnumerable<CaseReadDto>> GetAllCasesAsync() => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<CaseReadDto> GetCaseByIdAsync(int @case) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> CreateCase(CaseInsertDto @case) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> UpdateCase(int id, CaseUpdateDto @case) => throw new NotImplementedException();

		/// <inheritdoc />
		public Task<bool> DeleteCase(int id) => throw new NotImplementedException();

		#endregion
	}
}

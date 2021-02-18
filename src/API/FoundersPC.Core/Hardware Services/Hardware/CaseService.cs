#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware;
using FoundersPC.Domain.Entities.Hardware;
using FoundersPC.Infrastructure.API.UoW;

#endregion

namespace FoundersPC.Services.Hardware_Services.Hardware
{
	public class CaseService : ICaseService
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWorkAPIAsync _unitOfWorkApi;

		public CaseService(IUnitOfWorkAPIAsync unitOfWorkApi, IMapper mapper)
		{
			_unitOfWorkApi = unitOfWorkApi;
			_mapper = mapper;
		}

		#region Implementation of ICaseService

		/// <inheritdoc />
		public async Task<IEnumerable<CaseReadDto>> GetAllCasesAsync() => _mapper.Map<IEnumerable<Case>, IEnumerable<CaseReadDto>>(await _unitOfWorkApi
																																		 .CasesRepository
																																		 .GetAllAsync());

		/// <inheritdoc />
		public async Task<CaseReadDto> GetCaseByIdAsync(int caseId) =>
				_mapper.Map<Case, CaseReadDto>(await _unitOfWorkApi.CasesRepository.GetByIdAsync(caseId));

		/// <inheritdoc />
		public async Task<bool> CreateCase(CaseInsertDto @case)
		{
			var mappedCase = _mapper.Map<CaseInsertDto, Case>(@case);
			await _unitOfWorkApi.CasesRepository.AddAsync(mappedCase);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> UpdateCase(int id, CaseUpdateDto @case)
		{
			var bdEntity = await _unitOfWorkApi.CasesRepository.GetByIdAsync(id);

			if (bdEntity == null) return false;

			_mapper.Map(@case, bdEntity);
			await _unitOfWorkApi.CasesRepository.UpdateAsync(bdEntity);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		/// <inheritdoc />
		public async Task<bool> DeleteCase(int id)
		{
			var caseToDelete = await _unitOfWorkApi.CasesRepository.GetByIdAsync(id);
			await _unitOfWorkApi.CasesRepository.DeleteAsync(caseToDelete);

			return await _unitOfWorkApi.SaveChangesAsync() > 0;
		}

		#endregion
	}
}
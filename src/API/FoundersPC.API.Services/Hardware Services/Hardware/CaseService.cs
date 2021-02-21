﻿#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using FoundersPC.API.Domain.Entities.Hardware;
using FoundersPC.API.Infrastructure.UoW;

#endregion

namespace FoundersPC.API.Services.Hardware_Services.Hardware
{
    public class CaseService : ICaseService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkHardwareAPI _unitOfWorkHardwareAPI;

        public CaseService(IUnitOfWorkHardwareAPI unitOfWorkHardwareAPI, IMapper mapper)
        {
            _unitOfWorkHardwareAPI = unitOfWorkHardwareAPI;
            _mapper = mapper;
        }

        #region Implementation of ICaseService

        /// <inheritdoc />
        public async Task<IEnumerable<CaseReadDto>> GetAllCasesAsync() =>
            _mapper.Map<IEnumerable<Case>, IEnumerable<CaseReadDto>>(await _unitOfWorkHardwareAPI
                                                                           .CasesRepository
                                                                           .GetAllAsync());

        /// <inheritdoc />
        public async Task<CaseReadDto> GetCaseByIdAsync(int caseId) =>
            _mapper.Map<Case, CaseReadDto>(await _unitOfWorkHardwareAPI.CasesRepository.GetByIdAsync(caseId));

        /// <inheritdoc />
        public async Task<bool> CreateCase(CaseInsertDto @case)
        {
            var mappedCase = _mapper.Map<CaseInsertDto, Case>(@case);

            var entityAlreadyExists = await _unitOfWorkHardwareAPI.CasesRepository.AnyAsync(x => x.Equals(mappedCase));

            if (entityAlreadyExists) return false;

            await _unitOfWorkHardwareAPI.CasesRepository.AddAsync(mappedCase);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> UpdateCase(int id, CaseUpdateDto @case)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.CasesRepository.GetByIdAsync(id);

            if (dataBaseEntity == null) return false;

            _mapper.Map(@case, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.CasesRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult) return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteCase(int id)
        {
            var deleteResult = await _unitOfWorkHardwareAPI.CasesRepository.DeleteAsync(id);

            if (!deleteResult) return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}
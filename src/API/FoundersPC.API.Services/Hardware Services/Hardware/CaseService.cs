#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using FoundersPC.API.Domain.Entities;
using FoundersPC.API.Dto;
using FoundersPC.API.Infrastructure.UnitOfWork;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.RequestResponseShared.Pagination;

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

        #region Implementation of IPaginateableService<CaseReadDto>

        /// <inheritdoc/>
        public async Task<IPaginationResponse<CaseReadDto>> GetPaginateableAsync(int pageNumber = 1, int pageSize = FoundersPCConstants.PageSize)
        {
            var items = _mapper.Map<IEnumerable<Case>, IEnumerable<CaseReadDto>>(await _unitOfWorkHardwareAPI
                                                                                       .CasesRepository
                                                                                       .GetPaginateableAsync(pageNumber, pageSize));

            var totalItemsCount = await _unitOfWorkHardwareAPI.CasesRepository.CountAsync();

            return new PaginationResponse<CaseReadDto>(items, totalItemsCount);
        }

        #endregion

        #region Implementation of ICaseService

        /// <inheritdoc/>
        public async Task<IEnumerable<CaseReadDto>> GetAllCasesAsync() =>
            _mapper.Map<IEnumerable<Case>, IEnumerable<CaseReadDto>>(await _unitOfWorkHardwareAPI
                                                                           .CasesRepository
                                                                           .GetAllAsync());

        /// <inheritdoc/>
        public async Task<CaseReadDto> GetCaseByIdAsync(int caseId) =>
            _mapper.Map<Case, CaseReadDto>(await _unitOfWorkHardwareAPI.CasesRepository.GetByIdAsync(caseId));

        /// <inheritdoc/>
        public async Task<bool> CreateCaseAsync(CaseInsertDto @case)
        {
            var mappedCase = _mapper.Map<CaseInsertDto, Case>(@case);

            var entityAlreadyExists = await _unitOfWorkHardwareAPI.CasesRepository.AnyAsync(x => x.Equals(mappedCase));

            if (entityAlreadyExists)
                return false;

            await _unitOfWorkHardwareAPI.CasesRepository.AddAsync(mappedCase);

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> UpdateCaseAsync(int id, CaseUpdateDto @case)
        {
            var dataBaseEntity = await _unitOfWorkHardwareAPI.CasesRepository.GetByIdAsync(id);

            if (dataBaseEntity == null)
                return false;

            _mapper.Map(@case, dataBaseEntity);
            var updateResult = await _unitOfWorkHardwareAPI.CasesRepository.UpdateAsync(dataBaseEntity);

            if (!updateResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        /// <inheritdoc/>
        public async Task<bool> DeleteCaseAsync(int id)
        {
            var deleteResult = await _unitOfWorkHardwareAPI.CasesRepository.DeleteAsync(id);

            if (!deleteResult)
                return false;

            return await _unitOfWorkHardwareAPI.SaveChangesAsync() > 0;
        }

        #endregion
    }
}
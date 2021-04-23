#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
    public interface ICasesManagingService
    {
        Task<IEnumerable<CaseReadDto>> GetAllCasesAsync(string managerToken);

        Task<CaseReadDto> GetCaseByIdAsync(int id, string managerToken);

        Task<bool> UpdateCaseAsync(int id, CaseUpdateDto @case, string managerToken);

        Task<bool> DeleteCaseAsync(int caseId, string managerToken);

        Task<bool> CreateCaseAsync(CaseInsertDto @case, string managerToken);

        Task<IPaginationResponse<CaseReadDto>> GetPaginateableCasesAsync(int pageNumber,
                                                                         int pageSize,
                                                                         string managerToken);
    }
}
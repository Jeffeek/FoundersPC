#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware
{
    public interface ICaseService
    {
        Task<IEnumerable<CaseReadDto>> GetAllCasesAsync();

        Task<CaseReadDto> GetCaseByIdAsync(int caseId);

        Task<bool> CreateCaseAsync(CaseInsertDto @case);

        Task<bool> UpdateCaseAsync(int id, CaseUpdateDto @case);

        Task<bool> DeleteCaseAsync(int id);
    }
}
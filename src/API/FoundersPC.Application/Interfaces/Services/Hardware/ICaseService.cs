#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace FoundersPC.Application.Interfaces.Services.Hardware
{
    public interface ICaseService
    {
        Task<IEnumerable<CaseReadDto>> GetAllCasesAsync();

        Task<CaseReadDto> GetCaseByIdAsync(int caseId);

        Task<bool> CreateCase(CaseInsertDto @case);

        Task<bool> UpdateCase(int id, CaseUpdateDto @case);

        Task<bool> DeleteCase(int id);
    }
}
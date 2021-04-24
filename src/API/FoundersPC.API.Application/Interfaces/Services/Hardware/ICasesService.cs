#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ServicesShared;

#endregion

namespace FoundersPC.API.Application.Interfaces.Services.Hardware
{
    /// <summary>
    ///     Interface for decoration of database logic with entities
    /// </summary>
    public interface ICasesService : IPaginateableService<CaseReadDto>
    {
        /// <summary>
        ///     Return an enumeration of all <see cref="CaseReadDto"/> entities
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CaseReadDto>> GetAllCasesAsync();

        Task<CaseReadDto> GetCaseByIdAsync(int caseId);

        Task<bool> CreateCaseAsync(CaseInsertDto @case);

        Task<bool> UpdateCaseAsync(int id, CaseUpdateDto @case);

        Task<bool> DeleteCaseAsync(int id);
    }
}
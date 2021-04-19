#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
    public interface IProcessorCoresManagingService
    {
        Task<IEnumerable<ProcessorCoreReadDto>> GetAllProcessorCoresAsync(string managerToken);

        Task<ProcessorCoreReadDto> GetProcessorCoreByIdAsync(int id, string managerToken);

        Task<bool> UpdateProcessorCoreAsync(int id, ProcessorCoreUpdateDto processorCore, string managerToken);

        Task<bool> DeleteProcessorCoreAsync(int processorCoreId, string managerToken);

        Task<bool> CreateProcessorCoreAsync(ProcessorCoreInsertDto processorCore, string managerToken);

        Task<IPaginationResponse<ProcessorCoreReadDto>> GetPaginateableProcessorCoresAsync(int pageNumber,
                                                                                           int pageSize,
                                                                                           string managerToken);
    }
}
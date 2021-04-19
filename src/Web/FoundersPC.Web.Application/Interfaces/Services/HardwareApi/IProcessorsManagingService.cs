#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
    public interface IProcessorsManagingService
    {
        Task<IEnumerable<ProcessorReadDto>> GetAllProcessorsAsync(string managerToken);

        Task<ProcessorReadDto> GetProcessorByIdAsync(int id, string managerToken);

        Task<bool> UpdateProcessorAsync(int id, ProcessorUpdateDto processor, string managerToken);

        Task<bool> DeleteProcessorAsync(int processorId, string managerToken);

        Task<bool> CreateProcessorAsync(ProcessorInsertDto processor, string managerToken);

        Task<IPaginationResponse<ProcessorReadDto>> GetPaginateableProcessorsAsync(int pageNumber,
                                                                                   int pageSize,
                                                                                   string managerToken);
    }
}
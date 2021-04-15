#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.Response.Pagination;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
    public interface IProducersManagingService
    {
        Task<IEnumerable<ProducerReadDto>> GetAllProducersAsync(string managerToken);

        Task<ProducerReadDto> GetProducerByIdAsync(int id, string managerToken);

        Task<bool> UpdateProducerAsync(int id, ProducerUpdateDto producer, string managerToken);

        Task<bool> DeleteProducerAsync(int producerId, string managerToken);

        Task<bool> CreateProducerAsync(ProducerInsertDto producer, string managerToken);

        Task<IPaginationResponse<ProducerReadDto>> GetPaginateableProducersAsync(int pageNumber,
                                                                                 int pageSize,
                                                                                 string managerToken);
    }
}
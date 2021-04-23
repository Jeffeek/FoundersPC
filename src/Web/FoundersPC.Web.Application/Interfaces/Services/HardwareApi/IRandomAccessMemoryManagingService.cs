#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.RequestResponseShared.Pagination.Response;

#endregion

namespace FoundersPC.Web.Application.Interfaces.Services.HardwareApi
{
    public interface IRandomAccessMemoryManagingService
    {
        Task<IEnumerable<RandomAccessMemoryReadDto>> GetAllRandomAccessMemoryAsync(string managerToken);

        Task<RandomAccessMemoryReadDto> GetRandomAccessMemoryByIdAsync(int id, string managerToken);

        Task<bool> UpdateRandomAccessMemoryAsync(int id, RandomAccessMemoryUpdateDto randomAccessMemory, string managerToken);

        Task<bool> DeleteRandomAccessMemoryAsync(int randomAccessMemoryId, string managerToken);

        Task<bool> CreateRandomAccessMemoryAsync(RandomAccessMemoryInsertDto randomAccessMemory, string managerToken);

        Task<IPaginationResponse<RandomAccessMemoryReadDto>> GetPaginateableRandomAccessMemoryAsync(int pageNumber,
                                                                                                    int pageSize,
                                                                                                    string managerToken);
    }
}
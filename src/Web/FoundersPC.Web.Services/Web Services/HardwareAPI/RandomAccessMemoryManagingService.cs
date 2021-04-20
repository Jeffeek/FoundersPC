using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.Pagination.Response;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.Extensions.Logging;

namespace FoundersPC.Web.Services.Web_Services.HardwareAPI
{
	public class RandomAccessMemoryManagingService : BoilerplaitManagingService<RandomAccessMemoryInsertDto, RandomAccessMemoryReadDto, RandomAccessMemoryUpdateDto>, IRandomAccessMemoryManagingService
	{
		public RandomAccessMemoryManagingService(IHttpClientFactory clientFactory,
		                                         ILogger<RandomAccessMemoryManagingService> logger) : base(clientFactory,
		                                                                                                   logger,
		                                                                                                   HardwareApiRoutes.RandomAccessMemoryEndpoint) { }

		/// <inheritdoc />
		public RandomAccessMemoryManagingService(IHttpClientFactory clientFactory, ILogger logger, string apiRoute) : base(clientFactory, logger, apiRoute) { }

		#region Implementation of IRandomAccessMemoryManagingService

		/// <inheritdoc />
		public Task<IEnumerable<RandomAccessMemoryReadDto>> GetAllRandomAccessMemoryAsync(string managerToken) =>
			GetAllAsync(managerToken);

		/// <inheritdoc />
		public Task<RandomAccessMemoryReadDto> GetRandomAccessMemoryByIdAsync(int id, string managerToken) =>
			GetByIdAsync(id, managerToken);

		/// <inheritdoc />
		public Task<bool> UpdateRandomAccessMemoryAsync(int id, RandomAccessMemoryUpdateDto randomAccessMemory, string managerToken) =>
			UpdateAsync(id, randomAccessMemory, managerToken);

		/// <inheritdoc />
		public Task<bool> DeleteRandomAccessMemoryAsync(int randomAccessMemoryId, string managerToken) =>
			DeleteAsync(randomAccessMemoryId, managerToken);

		/// <inheritdoc />
		public Task<bool> CreateRandomAccessMemoryAsync(RandomAccessMemoryInsertDto randomAccessMemory, string managerToken) =>
			CreateAsync(randomAccessMemory, managerToken);

		/// <inheritdoc />
		public Task<IPaginationResponse<RandomAccessMemoryReadDto>> GetPaginateableRandomAccessMemoryAsync(int pageNumber, int pageSize, string managerToken) =>
			GetPaginateableAsync(pageNumber, pageSize, managerToken);

		#endregion
	}
}

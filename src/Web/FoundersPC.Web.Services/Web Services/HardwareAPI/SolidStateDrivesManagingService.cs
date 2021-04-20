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
	public class SolidStateDrivesManagingService : BoilerplaitManagingService<SolidStateDriveInsertDto, SolidStateDriveReadDto, SolidStateDriveUpdateDto>, ISolidStateDrivesManagingService
	{
		public SolidStateDrivesManagingService(IHttpClientFactory clientFactory,
		                                       ILogger<SolidStateDrivesManagingService> logger) : base(clientFactory,
		                                                                                               logger,
		                                                                                               HardwareApiRoutes.SolidStateDrivesEndpoint) { }

		/// <inheritdoc />
		public SolidStateDrivesManagingService(IHttpClientFactory clientFactory, ILogger logger, string apiRoute) : base(clientFactory, logger, apiRoute) { }

		#region Implementation of ISolidStateDrivesManagingService

		/// <inheritdoc />
		public Task<IEnumerable<SolidStateDriveReadDto>> GetAllSolidStateDrivesAsync(string managerToken) =>
			GetAllAsync(managerToken);

		/// <inheritdoc />
		public Task<SolidStateDriveReadDto> GetSolidStateDriveByIdAsync(int id, string managerToken) =>
			GetByIdAsync(id, managerToken);

		/// <inheritdoc />
		public Task<bool> UpdateSolidStateDriveAsync(int id, SolidStateDriveUpdateDto solidStateDrive, string managerToken) =>
			UpdateAsync(id, solidStateDrive, managerToken);

		/// <inheritdoc />
		public Task<bool> DeleteSolidStateDriveAsync(int solidStateDriveId, string managerToken) =>
			DeleteAsync(solidStateDriveId, managerToken);

		/// <inheritdoc />
		public Task<bool> CreateSolidStateDriveAsync(SolidStateDriveInsertDto solidStateDrive, string managerToken) =>
			CreateAsync(solidStateDrive, managerToken);

		/// <inheritdoc />
		public Task<IPaginationResponse<SolidStateDriveReadDto>> GetPaginateableSolidStateDrivesAsync(int pageNumber, int pageSize, string managerToken) =>
			GetPaginateableAsync(pageNumber, pageSize, managerToken);

		#endregion
	}
}

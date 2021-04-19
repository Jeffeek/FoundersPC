#region Using namespaces

using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.Pagination.Response;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.Web.Services.Web_Services.HardwareAPI
{
    public class CasesManagingService : BoilerplaitManagingService<CaseInsertDto, CaseReadDto, CaseUpdateDto>,
                                        ICasesManagingService
    {
        /// <exception cref="T:System.ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
        public CasesManagingService(IHttpClientFactory clientFactory,
                                    ILogger<CasesManagingService> logger) : base(clientFactory,
                                                                                 logger,
                                                                                 HardwareApiRoutes.CasesEndpoint) { }

        #region Implementation of ICasesManagingService

        /// <inheritdoc/>
        public Task<IEnumerable<CaseReadDto>> GetAllCasesAsync(string managerToken) => GetAllAsync(managerToken);

        /// <inheritdoc/>
        public Task<CaseReadDto> GetCaseByIdAsync(int id, string managerToken) => GetByIdAsync(id, managerToken);

        /// <inheritdoc/>
        public Task<bool> UpdateCaseAsync(int id, CaseUpdateDto @case, string managerToken) => UpdateAsync(id, @case, managerToken);

        /// <inheritdoc/>
        public Task<bool> DeleteCaseAsync(int caseId, string managerToken) => DeleteAsync(caseId, managerToken);

        /// <inheritdoc/>
        public Task<bool> CreateCaseAsync(CaseInsertDto @case, string managerToken) => CreateAsync(@case, managerToken);

        /// <inheritdoc/>
        public Task<IPaginationResponse<CaseReadDto>> GetPaginateableCasesAsync(int pageNumber, int pageSize, string managerToken) =>
            GetPaginateableAsync(pageNumber, pageSize, managerToken);

        #endregion
    }
}
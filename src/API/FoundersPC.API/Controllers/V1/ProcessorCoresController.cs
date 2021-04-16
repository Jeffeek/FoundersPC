#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.Pagination.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/CPUCores")]
    [Route(HardwareApiRoutes.ProcessorCores)]
    public class ProcessorCoresController : Controller
    {
        private readonly ILogger<ProcessorCoresController> _logger;
        private readonly IProcessorCoreService _processorCoreService;

        public ProcessorCoresController(IProcessorCoreService processorCoreService,
                                        ILogger<ProcessorCoresController> logger)
        {
            _processorCoreService = processorCoreService;
            _logger = logger;
        }

        [HttpGet(ApplicationRestAddons.All)]
        public async Task<ActionResult<IEnumerable<ProcessorCoreReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _processorCoreService.GetAllProcessorCoresAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> GetPaginateable([FromQuery] PaginationRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            _logger.LogForPaginateableModelsRead(HttpContext, request.PageNumber, request.PageSize);

            return Json(await _processorCoreService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        [HttpGet(ApplicationRestAddons.GetById)]
        public async Task<ActionResult<ProcessorCoreReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var cpuCore = await _processorCoreService.GetProcessorCoreByIdAsync(id);

            return cpuCore == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(cpuCore);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async Task<ActionResult> Create([FromBody] ProcessorCoreInsertDto cpuCore)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _processorCoreService.CreateProcessorCoreAsync(cpuCore);

            return insertResult
                       ? Json(cpuCore)
                       : ResponseResultsHelper.InsertError("Error when tried to insert new cpu core");
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ProcessorCoreUpdateDto cpuCore)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _processorCoreService.UpdateProcessorCoreAsync(id, cpuCore);

            return result ? Json(cpuCore) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete(ApplicationRestAddons.Delete)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var readCpuCore = await _processorCoreService.GetProcessorCoreByIdAsync(id);

            if (readCpuCore == null)
                return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _processorCoreService.DeleteProcessorCoreAsync(id);

            return result ? Json(readCpuCore) : ResponseResultsHelper.DeleteError();
        }
    }
}
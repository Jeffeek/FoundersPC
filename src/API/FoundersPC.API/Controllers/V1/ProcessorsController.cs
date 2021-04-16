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
    [Route("HardwareApi/CPUs")]
    [Route(HardwareApiRoutes.Processors)]
    public class ProcessorsController : Controller
    {
        private readonly ICPUService _cpuService;
        private readonly ILogger<ProcessorsController> _logger;

        public ProcessorsController(ICPUService service, ILogger<ProcessorsController> logger)
        {
            _cpuService = service;
            _logger = logger;
        }

        [HttpGet(ApplicationRestAddons.All)]
        public async Task<ActionResult<IEnumerable<CPUReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _cpuService.GetAllCPUsAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> GetPaginateable([FromQuery] PaginationRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            _logger.LogForPaginateableModelsRead(HttpContext, request.PageNumber, request.PageSize);

            return Json(await _cpuService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        [HttpGet(ApplicationRestAddons.GetById)]
        public async Task<ActionResult<CPUReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var cpu = await _cpuService.GetCPUByIdAsync(id);

            return cpu == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(cpu);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async Task<ActionResult> Create([FromBody] CPUInsertDto cpu)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _cpuService.CreateCPUAsync(cpu);

            return insertResult ? Json(cpu) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CPUUpdateDto cpu)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _cpuService.UpdateCPUAsync(id, cpu);

            return result ? Json(cpu) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete(ApplicationRestAddons.Delete)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var readCpu = await _cpuService.GetCPUByIdAsync(id);

            if (readCpu == null)
                return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _cpuService.DeleteCPUAsync(id);

            return result ? Json(readCpu) : ResponseResultsHelper.DeleteError();
        }
    }
}
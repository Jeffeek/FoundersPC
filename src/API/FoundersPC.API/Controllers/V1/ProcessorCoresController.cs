#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("HardwareApi/ProcessorCores")]
    [Route("HardwareApi/CPUCores")]
    [ApiController]
    public class ProcessorCoresController : Controller
    {
        private readonly ILogger<ProcessorCoresController> _logger;
        private readonly IProcessorCoreService _service;

        public ProcessorCoresController(IProcessorCoreService service, ILogger<ProcessorCoresController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessorCoreReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _service.GetAllProcessorCoresAsync());
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<ProcessorCoreReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var cpuCore = await _service.GetProcessorCoreByIdAsync(id);

            return cpuCore == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(cpuCore);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] ProcessorCoreInsertDto cpuCore)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _service.CreateProcessorCoreAsync(cpuCore);

            return insertResult
                       ? Json(cpuCore)
                       : ResponseResultsHelper.InsertError("Error when tried to insert new cpu core");
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ProcessorCoreUpdateDto cpuCore)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _service.UpdateProcessorCoreAsync(id, cpuCore);

            return result ? Json(cpuCore) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var readCpuCore = await _service.GetProcessorCoreByIdAsync(id);

            if (readCpuCore == null)
                return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _service.DeleteProcessorCoreAsync(id);

            return result ? Json(readCpuCore) : ResponseResultsHelper.DeleteError();
        }
    }
}
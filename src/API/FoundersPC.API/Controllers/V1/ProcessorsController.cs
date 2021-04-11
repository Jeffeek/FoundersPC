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
    [ApiController]
    [Route("HardwareApi/Processors")]
    [Route("HardwareApi/Cpus")]
    public class ProcessorsController : Controller
    {
        private readonly ICPUService _cpuService;
        private readonly ILogger<ProcessorsController> _logger;

        public ProcessorsController(ICPUService service, ILogger<ProcessorsController> logger)
        {
            _cpuService = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CPUReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _cpuService.GetAllCPUsAsync());
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<CPUReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var cpu = await _cpuService.GetCPUByIdAsync(id);

            return cpu == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(cpu);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] CPUInsertDto cpu)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _cpuService.CreateCPUAsync(cpu);

            return insertResult ? Json(cpu) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CPUUpdateDto cpu)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _cpuService.UpdateCPUAsync(id, cpu);

            return result ? Json(cpu) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var readCpu = await _cpuService.GetCPUByIdAsync(id);

            if (readCpu == null) return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _cpuService.DeleteCPUAsync(id);

            return result ? Json(readCpu) : ResponseResultsHelper.DeleteError();
        }
    }
}
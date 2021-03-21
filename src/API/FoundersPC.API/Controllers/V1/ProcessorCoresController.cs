#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [Authorize]
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessorCoreReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _service.GetAllProcessorCoresAsync());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessorCoreReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelRead(HttpContext, id.Value);

            var cpuCore = await _service.GetProcessorCoreByIdAsync(id.Value);

            return cpuCore == null ? ResponseResultsHelper.NotFoundByIdResult(id.Value) : Json(cpuCore);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] ProcessorCoreInsertDto cpuCore)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _service.CreateProcessorCoreAsync(cpuCore);

            return insertResult
                       ? Json(cpuCore)
                       : ResponseResultsHelper.InsertError("Error when tried to insert new cpu core");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] ProcessorCoreUpdateDto cpuCore)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult("null");
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id.Value);

            var result = await _service.UpdateProcessorCoreAsync(id.Value, cpuCore);

            return result ? Json(cpuCore) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult("null");

            _logger.LogForModelDelete(HttpContext, id.Value);

            var readCpuCore = await _service.GetProcessorCoreByIdAsync(id.Value);

            if (readCpuCore == null) return ResponseResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _service.DeleteProcessorCoreAsync(id.Value);

            return result ? Json(readCpuCore) : ResponseResultsHelper.DeleteError();
        }
    }
}
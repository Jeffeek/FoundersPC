#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/processorcores")]
    [ApiController]
    public class ProcessorCoresController : Controller
    {
        private readonly IProcessorCoreService _service;

        public ProcessorCoresController(IProcessorCoreService service) => _service = service;

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessorCoreReadDto>>> Get() => Json(await _service.GetAllProcessorCoresAsync());

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessorCoreReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var cpuCore = await _service.GetProcessorCoreByIdAsync(id.Value);

            return cpuCore == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(cpuCore);
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] ProcessorCoreInsertDto cpuCore)
        {
            if (!TryValidateModel(cpuCore)) return ValidationProblem(ModelState);

            var insertResult = await _service.CreateProcessorCore(cpuCore);

            return insertResult
                       ? Json(cpuCore)
                       : ResultsHelper.InsertError("Error when tried to insert new cpu core");
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] ProcessorCoreUpdateDto cpuCore)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult("null");
            if (!TryValidateModel(cpuCore)) return ValidationProblem(ModelState);

            var result = await _service.UpdateProcessorCore(id.Value, cpuCore);

            return result ? Json(cpuCore) : ResultsHelper.UpdateError();
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult("null");

            var readCpuCore = await _service.GetProcessorCoreByIdAsync(id.Value);

            if (readCpuCore == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _service.DeleteProcessorCore(id.Value);

            return result ? Json(readCpuCore) : ResultsHelper.DeleteError();
        }
    }
}
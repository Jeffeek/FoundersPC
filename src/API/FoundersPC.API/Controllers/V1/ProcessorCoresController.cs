#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [Route("api/processorcores")]
    [ApiController]
    [Authorize]
    public class ProcessorCoresController : ControllerBase
    {
        private readonly IProcessorCoreService _service;

        public ProcessorCoresController(IProcessorCoreService service) => _service = service;

        [Authorize(Roles = "Admin,Manager,DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessorCoreReadDto>>> Get() =>
            Ok(await _service.GetAllProcessorCoresAsync());

        [Authorize(Roles = "Admin,Manager,DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessorCoreReadDto>> Get(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var cpuCore = await _service.GetProcessorCoreByIdAsync(id.Value);

            if (cpuCore == null) return NotFound(id);

            return Ok(cpuCore);
        }

        [Authorize(Roles = "Admin,Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert(ProcessorCoreInsertDto cpuCore)
        {
            if (!TryValidateModel(cpuCore)) return ValidationProblem(ModelState);

            var insertResult = await _service.CreateProcessorCore(cpuCore);

            return !insertResult ? Problem() : Ok(cpuCore);
        }

        [Authorize(Roles = "Admin,Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, ProcessorCoreUpdateDto cpuCore)
        {
            if (!id.HasValue) return BadRequest(nameof(id));
            if (!TryValidateModel(cpuCore)) return ValidationProblem(ModelState);

            var result = await _service.UpdateProcessorCore(id.Value, cpuCore);

            if (!result) return Problem();

            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest(nameof(id));

            var readCpuCore = await _service.GetProcessorCoreByIdAsync(id.Value);

            if (readCpuCore == null) return NotFound(id);

            var result = await _service.DeleteProcessorCore(id.Value);

            return result ? Ok(readCpuCore) : Problem();
        }

    }
}
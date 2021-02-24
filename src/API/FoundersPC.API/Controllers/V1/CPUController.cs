#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/cpus")]
    [Authorize]
    public class CPUController : ControllerBase
    {
        private readonly ICPUService _cpuService;
        private readonly IMapper _mapper;

        public CPUController(ICPUService service, IMapper mapper)
        {
            _cpuService = service;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin,Manager,DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CPUReadDto>>> Get() => Ok(await _cpuService.GetAllCPUsAsync());

        [Authorize(Roles = "Admin,Manager,DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CPUReadDto>> Get(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var cpu = await _cpuService.GetCPUByIdAsync(id.Value);

            if (cpu == null) return NotFound(id);

            return Ok(cpu);
        }

        [Authorize(Roles = "Admin,Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert(CPUInsertDto cpu)
        {
            if (!TryValidateModel(cpu)) return ValidationProblem(ModelState);

            var insertResult = await _cpuService.CreateCPU(cpu);

            return !insertResult ? Problem() : Ok(cpu);
        }

        [Authorize(Roles = "Admin,Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, CPUUpdateDto cpu)
        {
            if (!id.HasValue) return BadRequest(nameof(id));
            if (!TryValidateModel(cpu)) return ValidationProblem(ModelState);

            var result = await _cpuService.UpdateCPU(id.Value, cpu);

            if (!result) return Problem();

            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest(nameof(id));

            var readCpu = await _cpuService.GetCPUByIdAsync(id.Value);

            if (readCpu == null) return NotFound(id);

            var result = await _cpuService.DeleteCPU(id.Value);

            return result ? Ok(readCpu) : Problem();
        }
    }
}
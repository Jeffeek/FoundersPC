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
    public class CPUController : ControllerBase
    {
        private readonly ICPUService _cpuService;
        private readonly IMapper _mapper;

        public CPUController(ICPUService service, IMapper mapper)
        {
            _cpuService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CPUReadDto>>> Get() => Ok(await _cpuService.GetAllCPUsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<CPUReadDto>> Get(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var cpu = await _cpuService.GetCPUByIdAsync(id.Value);

            if (cpu == null) return NotFound();

            return Ok(cpu);
        }

        //[Authorize(Policy = "Administrator")]
        [HttpPost]
        public async Task<ActionResult> Insert(CPUInsertDto cpu)
        {
            if (!TryValidateModel(cpu)) return ValidationProblem(ModelState);

            var insertResult = await _cpuService.CreateCPU(cpu);

            return !insertResult ? Problem() : Ok(cpu);
        }

        //[Authorize(Roles = "Administrator")]
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest(nameof(id));

            var readCpu = await _cpuService.GetCPUByIdAsync(id.Value);

            if (readCpu == null) return NotFound(id);

            var result = await _cpuService.DeleteCPU(id.Value);

            return result ? Ok(readCpu) : Problem();
        }

        //TODO: Works, but need to close a dependency between grabbing a full list of objects from service. And just send a ready "Update" object
        //[HttpPatch("{id}")]
        //public async Task<ActionResult> PartialUpdate(int? id, JsonPatchDocument<CPUUpdateDto> patchDocument)
        //{
        //	if (!id.HasValue) return BadRequest(nameof(id));
        //	var readDto = await _cpuService.GetCPUByIdAsync(id.Value);
        //	if (readDto == null) return NotFound(id);

        //	var cpuToPatch = _mapper.Map<CPUUpdateDto>(readDto);
        //	patchDocument.ApplyTo(cpuToPatch, ModelState);
        //	if (!TryValidateModel(cpuToPatch)) return ValidationProblem(ModelState);

        //	var result = await _cpuService.UpdateCPU(id.Value, cpuToPatch);
        //	if (!result) return Problem("Database has not changed");
        //	return Ok();
        //}
    }
}
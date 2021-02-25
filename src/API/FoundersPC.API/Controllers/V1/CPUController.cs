#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/cpus")]
    public class CPUController : Controller
    {
        private readonly ICPUService _cpuService;
        private readonly IMapper _mapper;

        public CPUController(ICPUService service, IMapper mapper)
        {
            _cpuService = service;
            _mapper = mapper;
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CPUReadDto>>> Get() => Json(await _cpuService.GetAllCPUsAsync());

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CPUReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var cpu = await _cpuService.GetCPUByIdAsync(id.Value);

            return cpu == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(cpu);
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] CPUInsertDto cpu)
        {
            if (!TryValidateModel(cpu)) return ValidationProblem(ModelState);

            var insertResult = await _cpuService.CreateCPU(cpu);

            return insertResult ? Json(cpu) : ResultsHelper.InsertError();
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] CPUUpdateDto cpu)
        {
            if (!id.HasValue) return ResultsHelper.UpdateError();
            if (!TryValidateModel(cpu)) return ValidationProblem(ModelState);

            var result = await _cpuService.UpdateCPU(id.Value, cpu);

            return result ? Json(cpu) : ResultsHelper.UpdateError();
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var readCpu = await _cpuService.GetCPUByIdAsync(id.Value);

            if (readCpu == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _cpuService.DeleteCPU(id.Value);

            return result ? Json(readCpu) : ResultsHelper.DeleteError();
        }
    }
}
#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/processors")]
    [Route("api/cpus")]
    public class ProcessorsController : Controller
    {
        private readonly ICPUService _cpuService;
        private readonly IMapper _mapper;

        public ProcessorsController(ICPUService service, IMapper mapper)
        {
            _cpuService = service;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Roles = "Administrator, Manager, DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CPUReadDto>>> Get() => Json(await _cpuService.GetAllCPUsAsync());

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Roles = "Administrator, Manager, DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CPUReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var cpu = await _cpuService.GetCPUByIdAsync(id.Value);

            return cpu == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(cpu);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Roles = "Administrator, Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] CPUInsertDto cpu)
        {
            if (!TryValidateModel(cpu)) return ValidationProblem(ModelState);

            var insertResult = await _cpuService.CreateCPUAsync(cpu);

            return insertResult ? Json(cpu) : ResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Roles = "Administrator, Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] CPUUpdateDto cpu)
        {
            if (!id.HasValue) return ResultsHelper.UpdateError();
            if (!TryValidateModel(cpu)) return ValidationProblem(ModelState);

            var result = await _cpuService.UpdateCPUAsync(id.Value, cpu);

            return result ? Json(cpu) : ResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Roles = "Administrator, Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var readCpu = await _cpuService.GetCPUByIdAsync(id.Value);

            if (readCpu == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _cpuService.DeleteCPUAsync(id.Value);

            return result ? Json(readCpu) : ResultsHelper.DeleteError();
        }
    }
}
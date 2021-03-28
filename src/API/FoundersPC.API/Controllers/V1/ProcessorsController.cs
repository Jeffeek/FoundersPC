#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.CPU;
using FoundersPC.API.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/Processors")]
    [Route("HardwareApi/Cpus")]
    public class ProcessorsController : Controller
    {
        private readonly ICPUService _cpuService;
        private readonly ILogger<ProcessorsController> _logger;
        private readonly IMapper _mapper;

        public ProcessorsController(ICPUService service, IMapper mapper, ILogger<ProcessorsController> logger)
        {
            _cpuService = service;
            _mapper = mapper;
            _logger = logger;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CPUReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _cpuService.GetAllCPUsAsync());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<CPUReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelRead(HttpContext, id.Value);

            var cpu = await _cpuService.GetCPUByIdAsync(id.Value);

            return cpu == null ? ResponseResultsHelper.NotFoundByIdResult(id.Value) : Json(cpu);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] CPUInsertDto cpu)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _cpuService.CreateCPUAsync(cpu);

            return insertResult ? Json(cpu) : ResponseResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] CPUUpdateDto cpu)
        {
            if (!id.HasValue) return ResponseResultsHelper.UpdateError();
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id.Value);

            var result = await _cpuService.UpdateCPUAsync(id.Value, cpu);

            return result ? Json(cpu) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelDelete(HttpContext, id.Value);

            var readCpu = await _cpuService.GetCPUByIdAsync(id.Value);

            if (readCpu == null) return ResponseResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _cpuService.DeleteCPUAsync(id.Value);

            return result ? Json(readCpu) : ResponseResultsHelper.DeleteError();
        }
    }
}
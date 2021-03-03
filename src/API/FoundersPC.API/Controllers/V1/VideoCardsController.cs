#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

//todo: add logger
namespace FoundersPC.API.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/videocards")]
    [Route("api/gpus")]
    public class VideoCardsController : Controller
    {
        private readonly IGPUService _gpuService;
        private readonly IMapper _mapper;

        public VideoCardsController(IGPUService service, IMapper mapper)
        {
            _gpuService = service;
            _mapper = mapper;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GPUReadDto>>> Get() => Json(await _gpuService.GetAllGPUsAsync());

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<GPUReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var gpuReadDto = await _gpuService.GetGPUByIdAsync(id.Value);

            return gpuReadDto == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(gpuReadDto);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] GPUUpdateDto gpu)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();
            if (!TryValidateModel(gpu)) return ValidationProblem(ModelState);

            var result = await _gpuService.UpdateGPUAsync(id.Value, gpu);

            return result ? Json(gpu) : ResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] GPUInsertDto gpu)
        {
            if (!TryValidateModel(gpu)) return ValidationProblem(ModelState);

            var insertResult = await _gpuService.CreateGPUAsync(gpu);

            return insertResult ? Json(gpu) : ResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var gpuReadDto = await _gpuService.GetGPUByIdAsync(id.Value);

            if (gpuReadDto == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _gpuService.DeleteGPUAsync(id.Value);

            return result ? Json(gpuReadDto) : ResultsHelper.DeleteError();
        }
    }
}
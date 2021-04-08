﻿#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.GPU;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/VideoCards")]
    [Route("HardwareApi/GPUs")]
    public class VideoCardsController : Controller
    {
        private readonly IGPUService _gpuService;
        private readonly ILogger<VideoCardsController> _logger;
        private readonly IMapper _mapper;

        public VideoCardsController(IGPUService service, IMapper mapper, ILogger<VideoCardsController> logger)
        {
            _gpuService = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GPUReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _gpuService.GetAllGPUsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GPUReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelRead(HttpContext, id.Value);

            var gpuReadDto = await _gpuService.GetGPUByIdAsync(id.Value);

            return gpuReadDto == null ? ResponseResultsHelper.NotFoundByIdResult(id.Value) : Json(gpuReadDto);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] GPUUpdateDto gpu)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id.Value);

            var result = await _gpuService.UpdateGPUAsync(id.Value, gpu);

            return result ? Json(gpu) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] GPUInsertDto gpu)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _gpuService.CreateGPUAsync(gpu);

            return insertResult ? Json(gpu) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelDelete(HttpContext, id.Value);

            var gpuReadDto = await _gpuService.GetGPUByIdAsync(id.Value);

            if (gpuReadDto == null) return ResponseResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _gpuService.DeleteGPUAsync(id.Value);

            return result ? Json(gpuReadDto) : ResponseResultsHelper.DeleteError();
        }
    }
}
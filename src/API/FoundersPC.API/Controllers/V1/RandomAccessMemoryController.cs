#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [Authorize(Policy = ApplicationAuthorizationPolicies.AuthenticatedPolicy)]
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/RandomAccessMemory")]
    [Route("HardwareApi/RAMs")]
    public class RandomAccessMemoryController : Controller
    {
        private readonly ILogger<RandomAccessMemoryController> _logger;
        private readonly IMapper _mapper;
        private readonly IRAMService _ramService;

        public RandomAccessMemoryController(IRAMService ramService,
                                            IMapper mapper,
                                            ILogger<RandomAccessMemoryController> logger)
        {
            _ramService = ramService;
            _mapper = mapper;
            _logger = logger;
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RAMReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _ramService.GetAllRAMsAsync());
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<RAMReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelRead(HttpContext, id.Value);

            var ram = await _ramService.GetRAMByIdAsync(id.Value);

            return ram == null ? ResponseResultsHelper.NotFoundByIdResult(id.Value) : Json(ram);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPut("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] RAMUpdateDto ram)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id.Value);

            var result = await _ramService.UpdateRAMAsync(id.Value, ram);

            return result ? Json(ram) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] RAMInsertDto ram)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _ramService.CreateRAMAsync(ram);

            return insertResult ? Json(ram) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelDelete(HttpContext, id.Value);

            var readRAM = await _ramService.GetRAMByIdAsync(id.Value);

            if (readRAM == null) return ResponseResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _ramService.DeleteRAMAsync(id.Value);

            return result ? Json(readRAM) : ResponseResultsHelper.DeleteError();
        }
    }
}
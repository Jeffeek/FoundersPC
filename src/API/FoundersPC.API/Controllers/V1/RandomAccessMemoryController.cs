#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.ApplicationConstants.Routes;
using FoundersPC.RequestResponseShared.Pagination.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/RAMs")]
    [Route(HardwareApiRoutes.RandomAccessMemory)]
    public class RandomAccessMemoryController : Controller
    {
        private readonly ILogger<RandomAccessMemoryController> _logger;
        private readonly IRAMService _ramService;

        public RandomAccessMemoryController(IRAMService ramService,
                                            ILogger<RandomAccessMemoryController> logger)
        {
            _ramService = ramService;
            _logger = logger;
        }

        [HttpGet(ApplicationRestAddons.All)]
        public async Task<ActionResult<IEnumerable<RAMReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _ramService.GetAllRAMsAsync());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CaseReadDto>>> GetPaginateable([FromQuery] PaginationRequest request)
        {
            if (!ModelState.IsValid) return BadRequest();

            _logger.LogForPaginateableModelsRead(HttpContext, request.PageNumber, request.PageSize);

            return Json(await _ramService.GetPaginateableAsync(request.PageNumber, request.PageSize));
        }

        [HttpGet(ApplicationRestAddons.GetById)]
        public async Task<ActionResult<RAMReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var ram = await _ramService.GetRAMByIdAsync(id);

            return ram == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(ram);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut(ApplicationRestAddons.Update)]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] RAMUpdateDto ram)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _ramService.UpdateRAMAsync(id, ram);

            return result ? Json(ram) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost(ApplicationRestAddons.Create)]
        public async Task<ActionResult> Insert([FromBody] RAMInsertDto ram)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _ramService.CreateRAMAsync(ram);

            return insertResult ? Json(ram) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete(ApplicationRestAddons.Delete)]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var readRAM = await _ramService.GetRAMByIdAsync(id);

            if (readRAM == null)
                return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _ramService.DeleteRAMAsync(id);

            return result ? Json(readRAM) : ResponseResultsHelper.DeleteError();
        }
    }
}
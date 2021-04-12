#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware.Memory;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("HardwareApi/RandomAccessMemory")]
    [Route("HardwareApi/RAMs")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RAMReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _ramService.GetAllRAMsAsync());
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<RAMReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var ram = await _ramService.GetRAMByIdAsync(id);

            return ram == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(ram);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] RAMUpdateDto ram)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _ramService.UpdateRAMAsync(id, ram);

            return result ? Json(ram) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] RAMInsertDto ram)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _ramService.CreateRAMAsync(ram);

            return insertResult ? Json(ram) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete("{id:int:min(1)}")]
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
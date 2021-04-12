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
    [ApiController]
    [Route("HardwareApi/HardDrives")]
    [Route("HardwareApi/HDDs")]
    public class HardDrivesController : Controller
    {
        private readonly IHDDService _hddService;
        private readonly ILogger<HardDrivesController> _logger;

        public HardDrivesController(IHDDService hddService, ILogger<HardDrivesController> logger)
        {
            _hddService = hddService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HDDReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _hddService.GetAllHDDsAsync());
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<HDDReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);
            var hddReadDto = await _hddService.GetHDDByIdAsync(id);

            return hddReadDto == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(hddReadDto);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] HDDUpdateDto hdd)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _hddService.UpdateHDDAsync(id, hdd);

            return result ? Json(hdd) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] HDDInsertDto hdd)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _hddService.CreateHDDAsync(hdd);

            return insertResult ? Json(hdd) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var hddReadDto = await _hddService.GetHDDByIdAsync(id);

            if (hddReadDto == null)
                return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _hddService.DeleteHDDAsync(id);

            return result ? Json(hddReadDto) : ResponseResultsHelper.DeleteError();
        }
    }
}
#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
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
    [Route("HardwareApi/Producers")]
    public class ProducersController : Controller
    {
        private readonly ILogger<ProducersController> _logger;
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService service, ILogger<ProducersController> logger)
        {
            _producerService = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProducerReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _producerService.GetAllProducersAsync());
        }

        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<ProducerReadDto>> Get([FromRoute] int id)
        {
            _logger.LogForModelRead(HttpContext, id);

            var producer = await _producerService.GetProducerByIdAsync(id);

            return producer == null ? ResponseResultsHelper.NotFoundByIdResult(id) : Json(producer);
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ProducerUpdateDto producer)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _producerService.UpdateProducerAsync(id, producer);

            return result ? Json(producer) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] ProducerInsertDto producer)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _producerService.CreateProducerAsync(producer);

            return insertResult ? Json(producer) : ResponseResultsHelper.InsertError();
        }

        [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            _logger.LogForModelDelete(HttpContext, id);

            var readProducer = await _producerService.GetProducerByIdAsync(id);

            if (readProducer == null)
                return ResponseResultsHelper.NotFoundByIdResult(id);

            var result = await _producerService.DeleteProducerAsync(id);

            return result ? Json(readProducer) : ResponseResultsHelper.DeleteError();
        }
    }
}
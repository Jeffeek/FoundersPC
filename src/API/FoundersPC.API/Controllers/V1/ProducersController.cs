#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
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

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProducerReadDto>>> Get()
        {
            _logger.LogForModelsRead(HttpContext);

            return Json(await _producerService.GetAllProducersAsync());
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Readable")]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProducerReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelRead(HttpContext, id.Value);

            var producer = await _producerService.GetProducerByIdAsync(id.Value);

            return producer == null ? ResponseResultsHelper.NotFoundByIdResult(id.Value) : Json(producer);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] ProducerUpdateDto producer)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelUpdate(HttpContext, id);

            var result = await _producerService.UpdateProducerAsync(id, producer);

            return result ? Json(producer) : ResponseResultsHelper.UpdateError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] ProducerInsertDto producer)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            _logger.LogForModelInsert(HttpContext);

            var insertResult = await _producerService.CreateProducerAsync(producer);

            return insertResult ? Json(producer) : ResponseResultsHelper.InsertError();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
                   Policy = "Changeable")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int? id)
        {
            if (!id.HasValue) return ResponseResultsHelper.BadRequestWithIdResult();

            _logger.LogForModelDelete(HttpContext, id.Value);

            var readProducer = await _producerService.GetProducerByIdAsync(id.Value);

            if (readProducer == null) return ResponseResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _producerService.DeleteProducerAsync(id.Value);

            return result ? Json(readProducer) : ResponseResultsHelper.DeleteError();
        }
    }
}
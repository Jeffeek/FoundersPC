#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/producers")]
    public class ProducersController : Controller
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService service) => _producerService = service;

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProducerReadDto>>> Get() => Json(await _producerService.GetAllProducersAsync());

        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProducerReadDto>> Get(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var producer = await _producerService.GetProducerByIdAsync(id.Value);

            return producer == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(producer);
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, [FromBody] ProducerUpdateDto producer)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();
            if (!TryValidateModel(producer)) return ValidationProblem(ModelState);

            var result = await _producerService.UpdateProducer(id.Value, producer);

            return result ? Json(producer) : ResultsHelper.UpdateError();
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] ProducerInsertDto producer)
        {
            if (!TryValidateModel(producer)) return ValidationProblem(ModelState);

            var insertResult = await _producerService.CreateProducer(producer);

            return insertResult ? Json(producer) : ResultsHelper.InsertError();
        }

        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

            var readCase = await _producerService.GetProducerByIdAsync(id.Value);

            if (readCase == null) return ResultsHelper.NotFoundByIdResult(id.Value);

            var result = await _producerService.DeleteProducer(id.Value);

            return result ? Json(readCase) : ResultsHelper.DeleteError();
        }
    }
}
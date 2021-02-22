#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
    [ApiVersion("1.0", Deprecated = false)]
    [ApiController]
    [Route("api/producers")]
    [Authorize]
    public class ProducersController : ControllerBase
    {
        private readonly IProducerService _producerService;

        public ProducersController(IProducerService service) => _producerService = service;

        [Authorize(Roles = "Admin,Manager,DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProducerReadDto>>> Get() =>
            Ok(await _producerService.GetAllProducersAsync());

        [Authorize(Roles = "Admin,Manager,DefaultUser")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProducerReadDto>> Get(int? id)
        {
            if (!id.HasValue) return BadRequest();

            var producer = await _producerService.GetProducerByIdAsync(id.Value);

            if (producer == null) return NotFound();

            return Ok(producer);
        }

        [Authorize(Roles = "Admin,Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost("{id}", Order = 0)]
        public async Task<ActionResult> Update(int? id, ProducerUpdateDto producer)
        {
            if (!id.HasValue) return BadRequest(nameof(id));
            if (!TryValidateModel(producer)) return ValidationProblem(ModelState);

            var result = await _producerService.UpdateProducer(id.Value, producer);

            if (!result) return Problem();

            return NoContent();
        }

        [Authorize(Roles = "Admin,Manager")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpPost]
        public async Task<ActionResult> Insert(ProducerInsertDto producer)
        {
            if (!TryValidateModel(producer)) return ValidationProblem(ModelState);

            var insertResult = await _producerService.CreateProducer(producer);

            return !insertResult ? Problem() : Ok(producer);
        }

        [Authorize(Roles = "Administrator")]
        [ApiVersion("1.0", Deprecated = false)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (!id.HasValue) return BadRequest(nameof(id));

            var readCase = await _producerService.GetProducerByIdAsync(id.Value);

            if (readCase == null) return NotFound(id);

            var result = await _producerService.DeleteProducer(id.Value);

            return result ? Ok(readCase) : Problem();
        }
    }
}
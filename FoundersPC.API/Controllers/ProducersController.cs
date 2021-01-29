#region Using derectives

using System.Threading.Tasks;
using FoundersPC.Core.Hardware_API.Producers;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers
{
	[ApiController]
	[Route("api/producers")]
	public class ProducersController : ControllerBase
	{
		private readonly IProducerService _producerService;

		public ProducersController(IProducerService service) => _producerService = service;

		[HttpGet]
		public async Task<ActionResult> Get() => Ok(await _producerService.GetAllProducersAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var producer = await _producerService.GetProducerByIdAsync(id.Value);
            if (producer == null) return NotFound();
            return Ok(producer);
        }
    }
}
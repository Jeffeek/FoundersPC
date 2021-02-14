#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services;
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
		public async Task<ActionResult<IEnumerable<ProducerReadDto>>> Get() =>
			Ok(await _producerService.GetAllProducersAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult<ProducerReadDto>> Get(int? id)
		{
			if (!id.HasValue) return BadRequest();
			var producer = await _producerService.GetProducerByIdAsync(id.Value);
			if (producer == null) return NotFound();
			return Ok(producer);
		}
	}
}
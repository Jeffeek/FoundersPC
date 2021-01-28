#region Using derectives

using System.Threading.Tasks;
using FoundersPC.Core.Requests.Producers;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers
{
	[ApiController]
	[Route("api/producers")]
	public class ProducersController : ControllerBase
	{
		private readonly IProducerRequest _request;

		public ProducersController(IProducerRequest request) => _request = request;

		[HttpGet]
		public async Task<ActionResult> Get() => Ok(await _request.GetAllProducersAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int? id)
        {
            if (id.HasValue)
                return Ok(await _request.GetProducerByIdAsync(id.Value));

            return NotFound();
        }
    }
}
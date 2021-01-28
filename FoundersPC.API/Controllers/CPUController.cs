#region Using derectives

using System.Threading.Tasks;
using FoundersPC.Core.Requests.Processors;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers
{
	[ApiController]
	[Route("api/cpu")]
	public class CPUController : ControllerBase
	{
		private readonly ICPURequest _request;

		public CPUController(ICPURequest request) => _request = request;

		[HttpGet]
		public async Task<ActionResult> Get() => Ok(await _request.GetAllProducersAsync());

		[HttpGet("{id}")]
		public async Task<ActionResult> Get(int? id)
		{
			if (id.HasValue)
				return Ok(await _request.GetAllProducersAsync());

			return NotFound();
		}
	}
}
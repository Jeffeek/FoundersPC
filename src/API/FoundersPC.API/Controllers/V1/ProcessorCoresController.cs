#region Using derectives

using System.Collections.Generic;
using System.Threading.Tasks;
using FoundersPC.Application;
using FoundersPC.Application.Interfaces.Services.Hardware.CPU;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.API.Controllers.V1
{
	[ApiVersion("1.0", Deprecated = false)]
	[Route("api/processorcores")]
	[ApiController]
	public class ProcessorCoresController : ControllerBase
	{
		private readonly IProcessorCoreService _service;

		public ProcessorCoresController(IProcessorCoreService service) => _service = service;

		// GET: api/<ProcessorCoresController>
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProcessorCoreReadDto>>> Get() => Ok(await _service.GetAllProcessorCoresAsync());

		//// GET api/<ProcessorCoresController>/5
		//[HttpGet("{id}")]
		//public string Get(int id)
		//{
		//	return "value";
		//}

		//// POST api/<ProcessorCoresController>
		//[HttpPost]
		//public void Post([FromBody] string value)
		//{
		//}

		//// PUT api/<ProcessorCoresController>/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		//// DELETE api/<ProcessorCoresController>/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
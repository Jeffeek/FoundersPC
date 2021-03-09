#region Using namespaces

using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Application;
using FoundersPC.API.Application.Interfaces.Services.Hardware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

//todo: add logger
namespace FoundersPC.API.Controllers.V1
{
	[Authorize]
	[ApiVersion("1.0", Deprecated = false)]
	[ApiController]
	[Route("api/powersupplies")]
	[Route("api/psus")]
	public class PowerSuppliesController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IPowerSupplyService _powerSupplyService;

		public PowerSuppliesController(IPowerSupplyService service, IMapper mapper)
		{
			_powerSupplyService = service;
			_mapper = mapper;
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
				   Policy = "Readable")]
		[ApiVersion("1.0", Deprecated = false)]
		[HttpGet]
		public async Task<ActionResult<IEnumerable<PowerSupplyReadDto>>> Get() => Json(await _powerSupplyService.GetAllPowerSuppliesAsync());

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
				   Policy = "Readable")]
		[ApiVersion("1.0", Deprecated = false)]
		[HttpGet("{id}")]
		public async Task<ActionResult<PowerSupplyReadDto>> Get(int? id)
		{
			if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

			var powerSupplyReadDto = await _powerSupplyService.GetPowerSupplyByIdAsync(id.Value);

			return powerSupplyReadDto == null ? ResultsHelper.NotFoundByIdResult(id.Value) : Json(powerSupplyReadDto);
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
				   Policy = "Changeable")]
		[ApiVersion("1.0", Deprecated = false)]
		[HttpPut("{id}", Order = 0)]
		public async Task<ActionResult> Update(int? id, [FromBody] PowerSupplyUpdateDto powerSupply)
		{
			if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();
			if (!TryValidateModel(powerSupply)) return ValidationProblem(ModelState);

			var result = await _powerSupplyService.UpdatePowerSupplyAsync(id.Value, powerSupply);

			return result ? Json(powerSupply) : ResultsHelper.UpdateError();
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
				   Policy = "Changeable")]
		[ApiVersion("1.0", Deprecated = false)]
		[HttpPost]
		public async Task<ActionResult> Insert([FromBody] PowerSupplyInsertDto powerSupply)
		{
			if (!TryValidateModel(powerSupply)) return ValidationProblem(ModelState);

			var insertResult = await _powerSupplyService.CreatePowerSupplyAsync(powerSupply);

			return insertResult ? Json(powerSupply) : ResultsHelper.InsertError();
		}

		[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
				   Policy = "Changeable")]
		[ApiVersion("1.0", Deprecated = false)]
		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int? id)
		{
			if (!id.HasValue) return ResultsHelper.BadRequestWithIdResult();

			var powerSupplyReadDto = await _powerSupplyService.GetPowerSupplyByIdAsync(id.Value);

			if (powerSupplyReadDto == null) return ResultsHelper.NotFoundByIdResult(id.Value);

			var result = await _powerSupplyService.DeletePowerSupplyAsync(id.Value);

			return result ? Json(powerSupplyReadDto) : ResultsHelper.DeleteError();
		}
	}
}
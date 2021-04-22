#region Using namespaces

using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.Middleware;
using FoundersPC.Web.Application;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using FoundersPC.Web.Domain.Common;
using FoundersPC.Web.Domain.Common.Hardware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

#endregion

namespace FoundersPC.Web.Controllers.HardwareApiCRUD
{
    [Route("HardwareApiManaging/PowerSupplies")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ModelValidation]
    public class PowerSuppliesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IPowerSuppliesManagingService _powerSuppliesManagingService;

        public PowerSuppliesController(IPowerSuppliesManagingService powerSuppliesManagingService,
                                       IMapper mapper)
        {
            _powerSuppliesManagingService = powerSuppliesManagingService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create() =>
            View("PowerSupplyCreate",
                 new PowerSupplyDtoViewModel
                 {
                     Certificate80PLUS = true,
                     FanDiameter = 120,
                     Efficiency = 100,
                     IsModular = true,
                     IsCPU4PINEmpty = false,
                     IsCPU8PINEmpty = false,
                     IsEfficiencyEmpty = false,
                     MotherboardPowering = "20+4",
                     PFC = true,
                     Power = 500,
                     ProducerId = 1,
                     CPU4PIN = true,
                     CPU8PIN = true
                 });

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] PowerSupplyDtoViewModel powerSupply)
        {
            var dto = _mapper.Map<PowerSupplyDtoViewModel, PowerSupplyInsertDto>(powerSupply);

            var insertResult =
                await _powerSuppliesManagingService.CreatePowerSupplyAsync(dto, HttpContext.GetJwtTokenFromCookie());

            if (insertResult)
                return RedirectToAction("PowerSuppliesTable");

            return Problem();
        }

        [HttpGet("Table")]
        public async Task<ActionResult> PowerSuppliesTable([FromQuery] int pageNumber = 1)
        {
            var powerSupplies = await _powerSuppliesManagingService.GetPaginateablePowerSuppliesAsync(pageNumber,
                                                                                                      FoundersPCConstants.PageSize,
                                                                                                      HttpContext.GetJwtTokenFromCookie());

            var indexModel = new IndexViewModel<PowerSupplyReadDto>
                             {
                                 IsPaginationNeeded = true,
                                 PagedList = new StaticPagedList<PowerSupplyReadDto>(powerSupplies.Items,
                                                                                     pageNumber,
                                                                                     FoundersPCConstants.PageSize,
                                                                                     powerSupplies.TotalItemsCount)
                             };

            return View("PowerSuppliesTable", indexModel);
        }

        [HttpGet("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id)
        {
            var powerSupply =
                await _powerSuppliesManagingService.GetPowerSupplyByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            var producerUpdate = _mapper.Map<PowerSupplyReadDto, PowerSupplyUpdateDto>(powerSupply);

            var viewModel = _mapper.Map<PowerSupplyUpdateDto, PowerSupplyDtoViewModel>(producerUpdate);

            return View("PowerSupplyEdit", viewModel);
        }

        [HttpPost("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] PowerSupplyDtoViewModel powerSupply)
        {
            var dto = _mapper.Map<PowerSupplyDtoViewModel, PowerSupplyUpdateDto>(powerSupply);

            var result =
                await _powerSuppliesManagingService.UpdatePowerSupplyAsync(id, dto, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("PowerSuppliesTable");
        }

        [HttpGet("Remove/{id:int:min(1)}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await _powerSuppliesManagingService.DeletePowerSupplyAsync(id, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("PowerSuppliesTable");
        }
    }
}
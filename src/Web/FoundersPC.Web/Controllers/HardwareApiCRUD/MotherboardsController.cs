#region Using namespaces

using System;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.ApplicationShared.Middleware;
using FoundersPC.Web.Application;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using FoundersPC.Web.Domain.Common;
using FoundersPC.Web.Domain.Common.Hardware.Motherboard;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

#endregion

namespace FoundersPC.Web.Controllers.HardwareApiCRUD
{
    [Route("HardwareApiManaging/Motherboards")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ModelValidation]
    public class MotherboardsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMotherboardsManagingService _motherboardsManagingService;

        public MotherboardsController(IMotherboardsManagingService motherboardsManagingService,
                                      IMapper mapper)
        {
            _motherboardsManagingService = motherboardsManagingService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create() =>
            View("MotherboardCreate",
                 new MotherboardInsertDtoViewModel
                 {
                     Factor = "ATX",
                     ProducerId = 1,
                     Title = String.Empty,
                     AudioSupport = "Dolby AUDIO",
                     M2SlotsCount = 2,
                     PCIExpressVersion = "16x",
                     PS2Support = true,
                     RAMMode = "2x",
                     RAMSlots = 4,
                     RAMSupport = "DDR4",
                     WiFiSupport = false,
                     SLIOrCrossfire = false,
                     Socket = "LGA1151"
                 });

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] MotherboardInsertDtoViewModel motherboard)
        {
            var dto = _mapper.Map<MotherboardInsertDtoViewModel, MotherboardInsertDto>(motherboard);

            var insertResult =
                await _motherboardsManagingService.CreateMotherboardAsync(dto, HttpContext.GetJwtTokenFromCookie());

            if (insertResult)
                return RedirectToAction("MotherboardsTable");

            return Problem();
        }

        [HttpGet("Table")]
        public async Task<ActionResult> MotherboardsTable([FromQuery] int pageNumber = 1)
        {
            var motherboards = await _motherboardsManagingService.GetPaginateableMotherboardsAsync(pageNumber,
                                                                                                   FoundersPCConstants.PageSize,
                                                                                                   HttpContext.GetJwtTokenFromCookie());

            var indexModel = new IndexViewModel<MotherboardReadDto>
                             {
                                 IsPaginationNeeded = true,
                                 PagedList = new StaticPagedList<MotherboardReadDto>(motherboards.Items,
                                                                                     pageNumber,
                                                                                     FoundersPCConstants.PageSize,
                                                                                     motherboards.TotalItemsCount)
                             };

            return View("MotherboardsTable", indexModel);
        }

        [HttpGet("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id)
        {
            var motherboard =
                await _motherboardsManagingService.GetMotherboardByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            var viewModel =
                _mapper.Map<MotherboardUpdateDto, MotherboardUpdateDtoViewModel>(_mapper.Map<MotherboardReadDto, MotherboardUpdateDto>(motherboard));

            return View("MotherboardEdit", viewModel);
        }

        [HttpPost("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] MotherboardUpdateDtoViewModel motherboard)
        {
            var dto = _mapper.Map<MotherboardUpdateDtoViewModel, MotherboardUpdateDto>(motherboard);

            var result =
                await _motherboardsManagingService.UpdateMotherboardAsync(id, dto, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("MotherboardsTable");
        }

        [HttpGet("Remove/{id:int:min(1)}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await _motherboardsManagingService.DeleteMotherboardAsync(id, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("MotherboardsTable");
        }
    }
}
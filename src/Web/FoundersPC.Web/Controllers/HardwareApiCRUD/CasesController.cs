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
using FoundersPC.Web.Domain.Common.Hardware.Cases;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

#endregion

namespace FoundersPC.Web.Controllers.HardwareApiCRUD
{
    [Route("HardwareApiManaging/Cases")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ModelValidation]
    public class CasesController : Controller
    {
        private readonly ICasesManagingService _casesManagingService;
        private readonly IMapper _mapper;

        public CasesController(ICasesManagingService casesManagingService,
                               IMapper mapper)
        {
            _casesManagingService = casesManagingService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create() =>
            View("CaseCreate",
                 new CaseInsertDtoViewModel
                 {
                     Color = "White",
                     Depth = 200,
                     Height = 120,
                     Width = 50,
                     Weight = 3.0,
                     IsDepthEmpty = false,
                     IsHeightEmpty = false,
                     IsWeightEmpty = false,
                     IsWidthEmpty = false,
                     Material = "Steel",
                     MaxMotherboardSize = "ATX",
                     ProducerId = 1,
                     TransparentWindow = false,
                     Title = String.Empty,
                     WindowMaterial = "Steel",
                     Type = "Middle-tower"
                 });

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CaseInsertDtoViewModel @case)
        {
            var dto = _mapper.Map<CaseInsertDtoViewModel, CaseInsertDto>(@case);

            var insertResult =
                await _casesManagingService.CreateCaseAsync(dto, HttpContext.GetJwtTokenFromCookie());

            if (insertResult)
                return RedirectToAction("CasesTable");

            return Problem();
        }

        [HttpGet("Table")]
        public async Task<ActionResult> CasesTable([FromQuery] int pageNumber = 1)
        {
            var cases = await _casesManagingService.GetPaginateableCasesAsync(pageNumber,
                                                                              FoundersPCConstants.PageSize,
                                                                              HttpContext.GetJwtTokenFromCookie());

            var indexModel = new IndexViewModel<CaseReadDto>
                             {
                                 IsPaginationNeeded = true,
                                 PagedList = new StaticPagedList<CaseReadDto>(cases.Items,
                                                                              pageNumber,
                                                                              FoundersPCConstants.PageSize,
                                                                              cases.TotalItemsCount)
                             };

            return View("CasesTable", indexModel);
        }

        [HttpGet("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id)
        {
            var @case =
                await _casesManagingService.GetCaseByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            var viewModel = _mapper.Map<CaseUpdateDto, CaseUpdateDtoViewModel>(_mapper.Map<CaseReadDto, CaseUpdateDto>(@case));

            return View("ProducerEdit", viewModel);
        }

        [HttpPost("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] CaseUpdateDtoViewModel @case)
        {
            var dto = _mapper.Map<CaseUpdateDtoViewModel, CaseUpdateDto>(@case);

            var result =
                await _casesManagingService.UpdateCaseAsync(id, dto, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("CasesTable");
        }

        [HttpGet("Remove/{id:int:min(1)}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await _casesManagingService.DeleteCaseAsync(id, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("CasesTable");
        }
    }
}
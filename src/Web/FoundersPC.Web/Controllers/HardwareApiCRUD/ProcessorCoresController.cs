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
using FoundersPC.Web.Domain.Common.Hardware.ProcessorCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

#endregion

namespace FoundersPC.Web.Controllers.HardwareApiCRUD
{
    [Route("HardwareApiManaging/ProcessorCores")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ModelValidation]
    public class ProcessorCoresController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProcessorCoresManagingService _processorCoresManagingService;

        public ProcessorCoresController(IProcessorCoresManagingService processorCoresManagingService,
                                        IMapper mapper)
        {
            _processorCoresManagingService = processorCoresManagingService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create() =>
            View("ProcessorCoreCreate",
                 new ProcessorCoreInsertDtoViewModel
                 {
                     Title = String.Empty,
                     Socket = "LGA1151",
                     IsMarketLaunchEmpty = true,
                     MarketLaunch = DateTime.Now,
                     L2Cache = 2048,
                     L3Cache = 6144,
                     MicroArchitecture = "Lake"
                 });

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ProcessorCoreInsertDtoViewModel processorCore)
        {
            var dto = _mapper.Map<ProcessorCoreInsertDtoViewModel, ProcessorCoreInsertDto>(processorCore);

            var insertResult =
                await _processorCoresManagingService.CreateProcessorCoreAsync(dto, HttpContext.GetJwtTokenFromCookie());

            if (insertResult)
                return RedirectToAction("ProcessorCoresTable");

            return Problem();
        }

        [HttpGet("Table")]
        public async Task<ActionResult> ProcessorCoresTable([FromQuery] int pageNumber = 1)
        {
            var processorCores = await _processorCoresManagingService.GetPaginateableProcessorCoresAsync(pageNumber,
                                                                                                         FoundersPCConstants.PageSize,
                                                                                                         HttpContext.GetJwtTokenFromCookie());

            var indexModel = new IndexViewModel<ProcessorCoreReadDto>
                             {
                                 IsPaginationNeeded = true,
                                 PagedList = new StaticPagedList<ProcessorCoreReadDto>(processorCores.Items,
                                                                                       pageNumber,
                                                                                       FoundersPCConstants.PageSize,
                                                                                       processorCores.TotalItemsCount)
                             };

            return View("ProcessorCoresTable", indexModel);
        }

        [HttpGet("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id)
        {
            var processorCore =
                await _processorCoresManagingService.GetProcessorCoreByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            var viewModel =
                _mapper.Map<ProcessorCoreUpdateDto, ProcessorCoreUpdateDtoViewModel>(_mapper.Map<ProcessorCoreReadDto, ProcessorCoreUpdateDto>(processorCore));

            return View("ProcessorCoreEdit", viewModel);
        }

        [HttpPost("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] ProcessorCoreUpdateDtoViewModel processorCore)
        {
            var dto = _mapper.Map<ProcessorCoreUpdateDtoViewModel, ProcessorCoreUpdateDto>(processorCore);

            var result =
                await _processorCoresManagingService.UpdateProcessorCoreAsync(id, dto, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("ProcessorCoresTable");
        }

        [HttpGet("Remove/{id:int:min(1)}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await _processorCoresManagingService.DeleteProcessorCoreAsync(id, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("ProcessorCoresTable");
        }
    }
}
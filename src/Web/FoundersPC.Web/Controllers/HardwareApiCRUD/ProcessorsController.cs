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
using FoundersPC.Web.Domain.Common.Hardware.Processor;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

#endregion

namespace FoundersPC.Web.Controllers.HardwareApiCRUD
{
    [Route("HardwareApiManaging/Processors")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ModelValidation]
    public class ProcessorsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProcessorsManagingService _processorsManagingService;

        public ProcessorsController(IProcessorsManagingService processorsManagingService,
                                    IMapper mapper)
        {
            _processorsManagingService = processorsManagingService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create() =>
            View("ProcessorCreate",
                 new ProcessorInsertDtoViewModel
                 {
                     ProducerId = 1,
                     Title = String.Empty,
                     Cores = 4,
                     Frequency = 4500,
                     IntegratedGraphics = true,
                     L1Cache = 1024,
                     MaxRamSpeed = 2666,
                     ProcessorCoreId = 1,
                     Series = "Series",
                     TurboBoostFrequency = 5000,
                     TDP = 65,
                     TechProcess = 14,
                     Threads = 8
                 });

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ProcessorInsertDtoViewModel processor)
        {
            var dto = _mapper.Map<ProcessorInsertDtoViewModel, ProcessorInsertDto>(processor);

            var insertResult =
                await _processorsManagingService.CreateProcessorAsync(dto, HttpContext.GetJwtTokenFromCookie());

            if (insertResult)
                return RedirectToAction("ProcessorsTable");

            return Problem();
        }

        [HttpGet("Table")]
        public async Task<ActionResult> ProcessorsTable([FromQuery] int pageNumber = 1)
        {
            var processors = await _processorsManagingService.GetPaginateableProcessorsAsync(pageNumber,
                                                                                             FoundersPCConstants.PageSize,
                                                                                             HttpContext.GetJwtTokenFromCookie());

            var indexModel = new IndexViewModel<ProcessorReadDto>
                             {
                                 IsPaginationNeeded = true,
                                 PagedList = new StaticPagedList<ProcessorReadDto>(processors.Items,
                                                                                   pageNumber,
                                                                                   FoundersPCConstants.PageSize,
                                                                                   processors.TotalItemsCount)
                             };

            return View("ProcessorsTable", indexModel);
        }

        [HttpGet("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id)
        {
            var processor =
                await _processorsManagingService.GetProcessorByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            var viewModel = _mapper.Map<ProcessorUpdateDto, ProcessorUpdateDtoViewModel>(_mapper.Map<ProcessorReadDto, ProcessorUpdateDto>(processor));

            return View("ProcessorEdit", viewModel);
        }

        [HttpPost("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] ProcessorUpdateDtoViewModel processor)
        {
            var dto = _mapper.Map<ProcessorUpdateDtoViewModel, ProcessorUpdateDto>(processor);

            var result =
                await _processorsManagingService.UpdateProcessorAsync(id, dto, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("ProcessorsTable");
        }

        [HttpGet("Remove/{id:int:min(1)}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await _processorsManagingService.DeleteProcessorAsync(id, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("ProcessorsTable");
        }
    }
}
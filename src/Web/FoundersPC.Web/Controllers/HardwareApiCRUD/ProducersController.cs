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
using FoundersPC.Web.Domain.Common.Hardware.Producer;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

#endregion

namespace FoundersPC.Web.Controllers.HardwareApiCRUD
{
    [Route("HardwareApiManaging/Producers")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ModelValidation]
    public class ProducersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProducersManagingService _producersManagingService;

        public ProducersController(IProducersManagingService producersManagingService,
                                   IMapper mapper)
        {
            _producersManagingService = producersManagingService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create() =>
            View("ProducerCreate",
                 new ProducerInsertDtoViewModel
                 {
                     Country = String.Empty,
                     FoundationDate = DateTime.Now,
                     IsFoundationDateEmpty = false,
                     FullName = "FullName",
                     IsShortNameEmpty = false,
                     ShortName = "ShortName",
                     Website = "https://",
                     IsWebsiteEmpty = false
                 });

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ProducerInsertDtoViewModel producer)
        {
            var dto = _mapper.Map<ProducerInsertDtoViewModel, ProducerInsertDto>(producer);

            var insertResult =
                await _producersManagingService.CreateProducerAsync(dto, HttpContext.GetJwtTokenFromCookie());

            if (insertResult)
                return RedirectToAction("ProducersTable");

            return Problem();
        }

        [HttpGet("Table")]
        public async Task<ActionResult> ProducersTable([FromQuery] int pageNumber = 1)
        {
            var producers = await _producersManagingService.GetPaginateableProducersAsync(pageNumber,
                                                                                          FoundersPCConstants.PageSize,
                                                                                          HttpContext.GetJwtTokenFromCookie());

            var indexModel = new IndexViewModel<ProducerReadDto>
                             {
                                 IsPaginationNeeded = true,
                                 PagedList = new StaticPagedList<ProducerReadDto>(producers.Items,
                                                                                  pageNumber,
                                                                                  FoundersPCConstants.PageSize,
                                                                                  producers.TotalItemsCount)
                             };

            return View("ProducersTable", indexModel);
        }

        [HttpGet("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id)
        {
            var producer =
                await _producersManagingService.GetProducerByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            var viewModel = _mapper.Map<ProducerUpdateDto, ProducerUpdateDtoViewModel>(_mapper.Map<ProducerReadDto, ProducerUpdateDto>(producer));

            return View("ProducerEdit", viewModel);
        }

        [HttpPost("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] ProducerUpdateDtoViewModel producer)
        {
            var dto = _mapper.Map<ProducerUpdateDtoViewModel, ProducerUpdateDto>(producer);

            var result =
                await _producersManagingService.UpdateProducerAsync(id, dto, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("ProducersTable");
        }

        [HttpGet("Remove/{id:int:min(1)}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await _producersManagingService.DeleteProducerAsync(id, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("ProducersTable");
        }
    }
}
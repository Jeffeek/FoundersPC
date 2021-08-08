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
using FoundersPC.Web.Domain.Common.Hardware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

#endregion

namespace FoundersPC.Web.Controllers.HardwareApiCRUD
{
    [Route("HardwareApiManaging/RandomAccessMemory")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ModelValidation]
    public class RandomAccessMemoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRandomAccessMemoryManagingService _randomAccessMemoryManagingService;

        public RandomAccessMemoryController(IRandomAccessMemoryManagingService randomAccessMemoryManagingService,
                                            IMapper mapper)
        {
            _randomAccessMemoryManagingService = randomAccessMemoryManagingService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create() =>
            View("RandomAccessMemoryCreate",
                 new RandomAccessMemoryDtoViewModel
                 {
                     ProducerId = 1,
                     Title = String.Empty,
                     ECC = true,
                     Frequency = 2400,
                     MemoryType = "DDR4",
                     PCIndex = 11,
                     Timings = "15-15-15",
                     Voltage = 1.2,
                     XMP = true
                 });

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] RandomAccessMemoryDtoViewModel randomAccessMemory)
        {
            var dto = _mapper.Map<RandomAccessMemoryDtoViewModel, RandomAccessMemoryInsertDto>(randomAccessMemory);

            var insertResult =
                await _randomAccessMemoryManagingService.CreateRandomAccessMemoryAsync(dto, HttpContext.GetJwtTokenFromCookie());

            if (insertResult)
                return RedirectToAction("RandomAccessMemoryTable");

            return Problem();
        }

        [HttpGet("Table")]
        public async Task<ActionResult> RandomAccessMemoryTable([FromQuery] int pageNumber = 1)
        {
            var randomAccessMemory = await _randomAccessMemoryManagingService.GetPaginateableRandomAccessMemoryAsync(pageNumber,
                                         FoundersPCConstants.PageSize,
                                         HttpContext.GetJwtTokenFromCookie());

            var indexModel = new IndexViewModel<RandomAccessMemoryReadDto>
                             {
                                 IsPaginationNeeded = true,
                                 PagedList = new StaticPagedList<RandomAccessMemoryReadDto>(randomAccessMemory.Items,
                                                                                            pageNumber,
                                                                                            FoundersPCConstants.PageSize,
                                                                                            randomAccessMemory.TotalItemsCount)
                             };

            return View("RandomAccessMemoryTable", indexModel);
        }

        [HttpGet("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id)
        {
            var randomAccessMemory =
                await _randomAccessMemoryManagingService.GetRandomAccessMemoryByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            var viewModel =
                _mapper.Map<RandomAccessMemoryUpdateDto, RandomAccessMemoryDtoViewModel>(_mapper
                                                                                             .Map<RandomAccessMemoryReadDto, RandomAccessMemoryUpdateDto
                                                                                             >(randomAccessMemory));

            return View("RandomAccessMemoryEdit", viewModel);
        }

        [HttpPost("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] RandomAccessMemoryDtoViewModel randomAccessMemory)
        {
            var dto = _mapper.Map<RandomAccessMemoryDtoViewModel, RandomAccessMemoryUpdateDto>(randomAccessMemory);

            var result =
                await _randomAccessMemoryManagingService.UpdateRandomAccessMemoryAsync(id, dto, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("RandomAccessMemoryTable");
        }

        [HttpGet("Remove/{id:int:min(1)}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await _randomAccessMemoryManagingService.DeleteRandomAccessMemoryAsync(id, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("RandomAccessMemoryTable");
        }
    }
}
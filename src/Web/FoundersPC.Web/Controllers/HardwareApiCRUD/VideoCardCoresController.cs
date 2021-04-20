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
using FoundersPC.Web.Domain.Common.Hardware.VideoCardCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

#endregion

namespace FoundersPC.Web.Controllers.HardwareApiCRUD
{
    [Route("HardwareApiManaging/VideoCardCores")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ModelValidation]
    public class VideoCardCoresController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVideoCardCoresManagingService _videoCardCoresManagingService;

        public VideoCardCoresController(IVideoCardCoresManagingService videoCardCoresManagingService,
                                        IMapper mapper)
        {
            _videoCardCoresManagingService = videoCardCoresManagingService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create() =>
            View("VideoCardCoreCreate",
                 new VideoCardCoreInsertDtoViewModel
                 {
                     Title = String.Empty,
                     SLIOrCrossfire = false,
                     ConnectionInterface = "PCI-E 16x",
                     DirectX = "12x",
                     MonitorsSupport = 4,
                     TechProcess = 14
                 });

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] VideoCardCoreInsertDtoViewModel videoCardCore)
        {
            var dto = _mapper.Map<VideoCardCoreInsertDtoViewModel, VideoCardCoreInsertDto>(videoCardCore);

            var insertResult =
                await _videoCardCoresManagingService.CreateVideoCardCoreAsync(dto, HttpContext.GetJwtTokenFromCookie());

            if (insertResult)
                return RedirectToAction("VideoCardCoresTable");

            return Problem();
        }

        [HttpGet("Table")]
        public async Task<ActionResult> VideoCardCoresTable([FromQuery] int pageNumber = 1)
        {
            var videoCardCores = await _videoCardCoresManagingService.GetPaginateableVideoCardCoresAsync(pageNumber,
                                                                                                         FoundersPCConstants.PageSize,
                                                                                                         HttpContext.GetJwtTokenFromCookie());

            var indexModel = new IndexViewModel<VideoCardCoreReadDto>
                             {
                                 IsPaginationNeeded = true,
                                 PagedList = new StaticPagedList<VideoCardCoreReadDto>(videoCardCores.Items,
                                                                                       pageNumber,
                                                                                       FoundersPCConstants.PageSize,
                                                                                       videoCardCores.TotalItemsCount)
                             };

            return View("VideoCardCoresTable", indexModel);
        }

        [HttpGet("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id)
        {
            var videoCardCore =
                await _videoCardCoresManagingService.GetVideoCardCoreByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            var viewModel =
                _mapper.Map<VideoCardCoreUpdateDto, VideoCardCoreUpdateDtoViewModel>(_mapper.Map<VideoCardCoreReadDto, VideoCardCoreUpdateDto>(videoCardCore));

            return View("VideoCardCoreEdit", viewModel);
        }

        [HttpPost("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] VideoCardCoreUpdateDtoViewModel videoCardCore)
        {
            var dto = _mapper.Map<VideoCardCoreUpdateDtoViewModel, VideoCardCoreUpdateDto>(videoCardCore);

            var result =
                await _videoCardCoresManagingService.UpdateVideoCardCoreAsync(id, dto, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("VideoCardCoresTable");
        }

        [HttpGet("Remove/{id:int:min(1)}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await _videoCardCoresManagingService.DeleteVideoCardCoreAsync(id, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("VideoCardCoresTable");
        }
    }
}
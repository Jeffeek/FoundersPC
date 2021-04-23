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
    [Route("HardwareApiManaging/VideoCards")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ModelValidation]
    public class VideoCardsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IVideoCardsManagingService _videoCardsManagingService;

        public VideoCardsController(IVideoCardsManagingService videoCardsManagingService,
                                    IMapper mapper)
        {
            _videoCardsManagingService = videoCardsManagingService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create() =>
            View("VideoCardCreate",
                 new VideoCardDtoViewModel
                 {
                     ProducerId = 1,
                     Title = String.Empty,
                     AdditionalPower = 0,
                     DisplayPort = 2,
                     DVI = 2,
                     GraphicsProcessorId = 1
                 });

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] VideoCardDtoViewModel videoCard)
        {
            var dto = _mapper.Map<VideoCardDtoViewModel, VideoCardInsertDto>(videoCard);

            var insertResult =
                await _videoCardsManagingService.CreateVideoCardAsync(dto, HttpContext.GetJwtTokenFromCookie());

            if (insertResult)
                return RedirectToAction("VideoCardsTable");

            return Problem();
        }

        [HttpGet("Table")]
        public async Task<ActionResult> VideoCardsTable([FromQuery] int pageNumber = 1)
        {
            var videoCards = await _videoCardsManagingService.GetPaginateableVideoCardsAsync(pageNumber,
                                                                                             FoundersPCConstants.PageSize,
                                                                                             HttpContext.GetJwtTokenFromCookie());

            var indexModel = new IndexViewModel<VideoCardReadDto>
                             {
                                 IsPaginationNeeded = true,
                                 PagedList = new StaticPagedList<VideoCardReadDto>(videoCards.Items,
                                                                                   pageNumber,
                                                                                   FoundersPCConstants.PageSize,
                                                                                   videoCards.TotalItemsCount)
                             };

            return View("VideoCardsTable", indexModel);
        }

        [HttpGet("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id)
        {
            var videoCard =
                await _videoCardsManagingService.GetVideoCardByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            var viewModel = _mapper.Map<VideoCardUpdateDto, VideoCardDtoViewModel>(_mapper.Map<VideoCardReadDto, VideoCardUpdateDto>(videoCard));

            return View("VideoCardEdit", viewModel);
        }

        [HttpPost("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] VideoCardDtoViewModel videoCard)
        {
            var dto = _mapper.Map<VideoCardDtoViewModel, VideoCardUpdateDto>(videoCard);

            var result =
                await _videoCardsManagingService.UpdateVideoCardAsync(id, dto, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("VideoCardsTable");
        }

        [HttpGet("Remove/{id:int:min(1)}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await _videoCardsManagingService.DeleteVideoCardAsync(id, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("VideoCardsTable");
        }
    }
}
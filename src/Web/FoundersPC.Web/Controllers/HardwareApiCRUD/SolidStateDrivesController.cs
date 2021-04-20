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
using FoundersPC.Web.Domain.Common.Hardware.SolidStateDrive;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

#endregion

namespace FoundersPC.Web.Controllers.HardwareApiCRUD
{
    [Route("HardwareApiManaging/SolidStateDrives")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ModelValidation]
    public class SolidStateDrivesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ISolidStateDrivesManagingService _solidStateDrivesManagingService;

        public SolidStateDrivesController(ISolidStateDrivesManagingService solidStateDrivesManagingService,
                                          IMapper mapper)
        {
            _solidStateDrivesManagingService = solidStateDrivesManagingService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create() =>
            View("SolidStateDriveCreate",
                 new SolidStateDriveInsertDtoViewModel
                 {
                     Factor = 2.5,
                     ProducerId = 1,
                     Title = String.Empty,
                     Interface = "SATA 3",
                     MicroScheme = "XCC15",
                     SequentialRead = 450,
                     SequentialRecording = 450,
                     Volume = 240
                 });

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] SolidStateDriveInsertDtoViewModel solidStateDrive)
        {
            var dto = _mapper.Map<SolidStateDriveInsertDtoViewModel, SolidStateDriveInsertDto>(solidStateDrive);

            var insertResult =
                await _solidStateDrivesManagingService.CreateSolidStateDriveAsync(dto, HttpContext.GetJwtTokenFromCookie());

            if (insertResult)
                return RedirectToAction("SolidStateDrivesTable");

            return Problem();
        }

        [HttpGet("Table")]
        public async Task<ActionResult> SolidStateDrivesTable([FromQuery] int pageNumber = 1)
        {
            var solidStateDrives = await _solidStateDrivesManagingService.GetPaginateableSolidStateDrivesAsync(pageNumber,
                                       FoundersPCConstants.PageSize,
                                       HttpContext.GetJwtTokenFromCookie());

            var indexModel = new IndexViewModel<SolidStateDriveReadDto>
                             {
                                 IsPaginationNeeded = true,
                                 PagedList = new StaticPagedList<SolidStateDriveReadDto>(solidStateDrives.Items,
                                                                                         pageNumber,
                                                                                         FoundersPCConstants.PageSize,
                                                                                         solidStateDrives.TotalItemsCount)
                             };

            return View("SolidStateDrivesTable", indexModel);
        }

        [HttpGet("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id)
        {
            var solidStateDrive =
                await _solidStateDrivesManagingService.GetSolidStateDriveByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            var viewModel =
                _mapper.Map<SolidStateDriveUpdateDto, SolidStateDriveUpdateDtoViewModel>(_mapper
                                                                                             .Map<SolidStateDriveReadDto, SolidStateDriveUpdateDto
                                                                                             >(solidStateDrive));

            return View("SolidStateDriveEdit", viewModel);
        }

        [HttpPost("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] SolidStateDriveUpdateDtoViewModel solidStateDrive)
        {
            var dto = _mapper.Map<SolidStateDriveUpdateDtoViewModel, SolidStateDriveUpdateDto>(solidStateDrive);

            var result =
                await _solidStateDrivesManagingService.UpdateSolidStateDriveAsync(id, dto, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("SolidStateDrivesTable");
        }

        [HttpGet("Remove/{id:int:min(1)}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await _solidStateDrivesManagingService.DeleteSolidStateDriveAsync(id, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("SolidStateDrivesTable");
        }
    }
}
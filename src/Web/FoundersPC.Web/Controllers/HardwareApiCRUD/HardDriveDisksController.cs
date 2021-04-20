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
using FoundersPC.Web.Domain.Common.Hardware.HardDriveDisk;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagedList.Core;

#endregion

namespace FoundersPC.Web.Controllers.HardwareApiCRUD
{
    [Route("HardwareApiManaging/HardDriveDisks")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    [ModelValidation]
    public class HardDriveDisksController : Controller
    {
        private readonly IHardDriveDisksManagingService _hardDriveDisksManagingService;
        private readonly IMapper _mapper;

        public HardDriveDisksController(IHardDriveDisksManagingService hardDriveDisksManagingService,
                                        IMapper mapper)
        {
            _hardDriveDisksManagingService = hardDriveDisksManagingService;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult Create() =>
            View("HardDriveDiskCreate",
                 new HardDriveDiskInsertDtoViewModel
                 {
                     BufferSize = 64,
                     Factor = 3.5,
                     HeadSpeed = 7200,
                     Interface = "SATA 3",
                     Noise = 30,
                     ProducerId = 1,
                     Title = String.Empty,
                     Volume = 1024
                 });

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] HardDriveDiskInsertDtoViewModel hardDriveDisk)
        {
            var dto = _mapper.Map<HardDriveDiskInsertDtoViewModel, HardDriveDiskInsertDto>(hardDriveDisk);

            var insertResult =
                await _hardDriveDisksManagingService.CreateHardDriveDiskAsync(dto, HttpContext.GetJwtTokenFromCookie());

            if (insertResult)
                return RedirectToAction("HardDriveDisksTable");

            return Problem();
        }

        [HttpGet("Table")]
        public async Task<ActionResult> HardDriveDisksTable([FromQuery] int pageNumber = 1)
        {
            var hardDriveDisks = await _hardDriveDisksManagingService.GetPaginateableHardDriveDisksAsync(pageNumber,
                                                                                                         FoundersPCConstants.PageSize,
                                                                                                         HttpContext.GetJwtTokenFromCookie());

            var indexModel = new IndexViewModel<HardDriveDiskReadDto>
                             {
                                 IsPaginationNeeded = true,
                                 PagedList = new StaticPagedList<HardDriveDiskReadDto>(hardDriveDisks.Items,
                                                                                       pageNumber,
                                                                                       FoundersPCConstants.PageSize,
                                                                                       hardDriveDisks.TotalItemsCount)
                             };

            return View("HardDriveDisksTable", indexModel);
        }

        [HttpGet("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id)
        {
            var hardDriveDisk =
                await _hardDriveDisksManagingService.GetHardDriveDiskByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            var viewModel =
                _mapper.Map<HardDriveDiskUpdateDto, HardDriveDiskUpdateDtoViewModel>(_mapper.Map<HardDriveDiskReadDto, HardDriveDiskUpdateDto>(hardDriveDisk));

            return View("HardDriveDiskEdit", viewModel);
        }

        [HttpPost("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] HardDriveDiskUpdateDtoViewModel hardDriveDisk)
        {
            var dto = _mapper.Map<HardDriveDiskUpdateDtoViewModel, HardDriveDiskUpdateDto>(hardDriveDisk);

            var result =
                await _hardDriveDisksManagingService.UpdateHardDriveDiskAsync(id, dto, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("HardDriveDisksTable");
        }

        [HttpGet("Remove/{id:int:min(1)}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await _hardDriveDisksManagingService.DeleteHardDriveDiskAsync(id, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("HardDriveDisksTable");
        }
    }
}
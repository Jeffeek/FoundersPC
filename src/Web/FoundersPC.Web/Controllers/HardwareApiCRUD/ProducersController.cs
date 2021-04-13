#region Using namespaces

using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared.ApplicationConstants;
using FoundersPC.Web.Application;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using FoundersPC.Web.Domain.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers.HardwareApiCRUD
{
    [Route("HardwareApiManaging/Producers")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy,
               AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
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
            View(new ProducerInsertDto
                 {
                     Country = String.Empty,
                     FoundationDate = null,
                     FullName = String.Empty,
                     ShortName = String.Empty,
                     Website = "https://"
                 });

        [HttpPost]
        public async Task<ActionResult> Create([FromForm] ProducerInsertDto producer)
        {
            var insertResult =
                await _producersManagingService.CreateProducerAsync(producer, HttpContext.GetJwtTokenFromCookie());

            if (insertResult)
                return RedirectToAction("Table");

            return Problem();
        }

        [HttpGet("Table")]
        public async Task<ActionResult> Table([FromQuery] int pageNumber = 1)
        {
            var producers = (await _producersManagingService.GetPaginateableProducersAsync(pageNumber,
                                                                                           FoundersPCConstants.PageSize,
                                                                                           HttpContext.GetJwtTokenFromCookie())).ToArray();

            var indexModel = new IndexViewModel<ProducerReadDto>
                             {
                                 Models = producers,
                                 Page = new PageViewModel(pageNumber,
                                                          producers.Length == FoundersPCConstants.PageSize)
                             };

            return View("Table", indexModel);
        }

        [HttpGet("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id)
        {
            var producer =
                await _producersManagingService.GetProducerByIdAsync(id, HttpContext.GetJwtTokenFromCookie());

            var producerUpdate = _mapper.Map<ProducerReadDto, ProducerUpdateDto>(producer);

            return View("Edit", producerUpdate);
        }

        [HttpPost("Edit/{id:int:min(1)}")]
        public async Task<ActionResult> Edit([FromRoute] int id, [FromForm] ProducerUpdateDto producer)
        {
            var result =
                await _producersManagingService.UpdateProducerAsync(id, producer, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("Table");
        }

        [HttpGet("Remove/{id:int:min(1)}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await _producersManagingService.DeleteProducerAsync(id, HttpContext.GetJwtTokenFromCookie());

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("Table");
        }
    }
}
#region Using namespaces

using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers.API
{
    [Route("Manager/Producers")]
    [Authorize(Policy = ApplicationAuthorizationPolicies.ManagerPolicy)]
    public class ProducersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProducersManagingService _producersManagingService;

        public ProducersController(IProducersManagingService producersManagingService, IMapper mapper)
        {
            _producersManagingService = producersManagingService;
            _mapper = mapper;
        }

        [Route("Table")]
        public async Task<ActionResult> Table()
        {
            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            var producers = await _producersManagingService.GetAllProducersAsync(token);

            return View("ProducersTable", producers);
        }

        [HttpGet]
        [Route("Edit/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            var producer = await _producersManagingService.GetProducerByIdAsync(id, token);

            var producerUpdate = _mapper.Map<ProducerReadDto, ProducerUpdateDto>(producer);

            return View("Edit", producerUpdate);
        }

        [Route("Edit/{id:int}")]
        [HttpPost]
        public async Task<ActionResult> Edit([FromRoute] int id, ProducerUpdateDto producer)
        {
            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            var result = await _producersManagingService.UpdateProducerAsync(id, producer, token);

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("Table");
        }

        [HttpGet]
        [Route("Remove/{id:int}")]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            var result = await _producersManagingService.DeleteProducerAsync(id, token);

            return !result ? RedirectToAction("ServerErrorIndex", "Error") : RedirectToAction("Table");
        }
    }
}
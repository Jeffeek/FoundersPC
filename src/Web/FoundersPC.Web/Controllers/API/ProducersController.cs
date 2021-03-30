#region Using namespaces

using System.Threading.Tasks;
using AutoMapper;
using FoundersPC.API.Dto;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers.API
{
    [Route("Manager/Producers")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,
               Roles = ApplicationRoles.AdministratorAndManager)]
    public class ProducersController : Controller
    {
        private readonly IProducersManagingService _producersManagingService;
        private readonly IMapper _mapper;

        public ProducersController(IProducersManagingService producersManagingService, IMapper mapper)
        {
            _producersManagingService = producersManagingService;
            _mapper = mapper;
        }

        public async Task<ActionResult> Table()
        {
            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            var producers = await _producersManagingService.GetAllProducersAsync(token);

            return View("ProducersTable", producers);
        }

        [Route("Details/{id:int}")]
        public async Task<ActionResult> Details(int id)
        {
            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            var producer = await _producersManagingService.GetProducerByIdAsync(id, token);

            return View("ProducerDetails", producer);
        }

        [Route("Edit/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            HttpContext.Request.Cookies.TryGetValue("token", out var token);

            var producer = await _producersManagingService.GetProducerByIdAsync(id, token);

            return View("ProducerEdit", _mapper.Map<ProducerReadDto, ProducerUpdateDto>(producer));
        }
    }
}
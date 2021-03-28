#region Using namespaces

using System.Threading.Tasks;
using FoundersPC.ApplicationShared;
using FoundersPC.Web.Application.Interfaces.Services.HardwareApi;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers.Administration
{
    [Route("Manager/Producers")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,
               Roles = ApplicationRoles.AdministratorAndManager)]
    public class ProducersWebController : Controller
    {
        private readonly IProducersManagingService _producersManagingService;

        public ProducersWebController(IProducersManagingService producersManagingService) =>
            _producersManagingService = producersManagingService;

        [Route("All")]
        public async Task<ActionResult> Index()
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

        //// GET: ProducersWebController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: ProducersWebController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ProducersWebController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: ProducersWebController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: ProducersWebController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: ProducersWebController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
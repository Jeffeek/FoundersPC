#region Using namespaces

using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers.Account
{
    public class RegisterController : Controller
    {
        public IActionResult Index() => View();
    }
}
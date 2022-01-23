#region Using namespaces

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#endregion

namespace FoundersPC.Web.Controllers;

[AllowAnonymous]
public class HomeController : Controller
{
    public IActionResult Index() => View();
}
using IdentityService3.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService3.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [SecurityHeaders]
    [Authorize]
    public class DiagnosticsController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var localAddresses = new string[] { "127.0.0.1", "::1", HttpContext.Connection.LocalIpAddress!.ToString() };
            if (!localAddresses.Contains(HttpContext.Connection.RemoteIpAddress!.ToString())) { return NotFound(); }

            var model = new DiagnosticsViewModel(await HttpContext.AuthenticateAsync());
            return View(model);
        }
    }
}

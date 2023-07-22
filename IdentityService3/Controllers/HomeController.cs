using IdentityServer4.Services;
using IdentityService3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService3.Controllers
{
    [SecurityHeaders]
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interaction"></param>
        /// <param name="environment"></param>
        /// <param name="logger"></param>
        public HomeController(IIdentityServerInteractionService interaction, IWebHostEnvironment environment, ILogger<HomeController> logger)
        {
            _interaction = interaction;
            _environment = environment;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            if (_environment.IsDevelopment()) { return View(); }
            _logger.LogInformation("Homepage is disabled in production. Returning 404.");
            return NotFound();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorId"></param>
        /// <returns></returns>
        public async Task<IActionResult> Error(string errorId)
        {
            var vm = new ErrorViewModel();
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                vm.Error = message;
                if (!_environment.IsDevelopment())
                {
                    message.ErrorDescription = null;
                }
            }
            return View("Error", vm);
        }
    }
}

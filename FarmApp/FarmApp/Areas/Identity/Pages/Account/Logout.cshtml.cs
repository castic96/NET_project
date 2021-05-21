using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FarmApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FarmApp.Areas.Identity.Pages.Account
{
    /// <summary>
    /// PageModel for logout.
    /// </summary>
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        /// <summary>
        /// Sign in manager.
        /// </summary>
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Logger.
        /// </summary>
        private readonly ILogger<LogoutModel> _logger;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="signInManager">Sign in manager.</param>
        /// <param name="logger">Logger.</param>
        public LogoutModel(SignInManager<User> signInManager, ILogger<LogoutModel> logger)
        {
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Processes user logout.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPost(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            else
            {
                return RedirectToPage();
            }
        }
    }
}

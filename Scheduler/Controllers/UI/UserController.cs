using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Scheduler.Data;
using Scheduler.Models;

namespace Scheduler.Controllers.UI
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<UserController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;

        public UserController(ApplicationDbContext context, IStringLocalizer<UserController> localizer, UserManager<ApplicationUser> userManager, IHostingEnvironment environment/*, ILogger logger*/)
        {
            _context = context;
            _localizer = localizer;
            _userManager = userManager;
            _environment = environment;
            //_logger = logger;
        }
        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index")});
            }
            return View();
        }

        [Authorize(Roles = "Patient")]
        public IActionResult Doctors()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Doctors") });
            }

            return View();
        }

        public async Task<IActionResult> Chat()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Doctors") });
            }
            var user = await GetCurrentUserAsync();
            return View(user);
        }

        public IActionResult Visits()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Visits") });
            }
            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
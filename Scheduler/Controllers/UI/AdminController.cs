using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Scheduler.Data;
using Scheduler.Models;

namespace Scheduler.Controllers.UI
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<AdminController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger _logger;

        public AdminController(ApplicationDbContext context, IStringLocalizer<AdminController> localizer, UserManager<ApplicationUser> userManager/*, ILogger logger*/)
        {
            _context = context;
            _localizer = localizer;
            _userManager = userManager;
            //_logger = logger;
        }

        public IActionResult DoctorsList()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index") });
            }
            return View();
        }

        public IActionResult PatientsList()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Index") });
            }
            return View();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Scheduler.Data;
using Scheduler.Models;
using Scheduler.ViewModels;
using Scheduler.ViewModels.HealthCenter;

namespace Scheduler.Controllers
{

    public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context, IStringLocalizer<HomeController> localizer)
        {
            _context = context;
            _localizer = localizer;
        }

        [HttpGet]
        public string Get()
        {
            // Retrieves the requested culture
            var rqf = Request.HttpContext.Features.Get<IRequestCultureFeature>();
            // Culture contains the information of the requested culture
            var culture = rqf.RequestCulture.Culture;

            return _localizer["About"];
        }

        public IActionResult Index()
        {
            var fourDoctors = _context.Doctors.OrderBy(d => Guid.NewGuid()).Take(4).Select(d => new DoctorViewModel
            {
                ID = d.ID,
                FirstName = d.FirstName,
                LastName = d.LastName,
                FullName = d.FullName,
                BirthDate = d.BirthDate,
                PhoneNumber = d.PhoneNumber,
                EMail = d.EMail,
                Address = d.Address,
                City = d.City,
                Sex = d.Sex,
                Notes = d.Notes,
                PersonalCode = d.PersonalCode,
            });

            var pageIndexViewModel = new PageIndexViewModel {
                Doctors = fourDoctors.ToList()
            };
            return View(pageIndexViewModel);
        }



        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contacts()
        {
            ViewData["Message"] = "Your contacts page.";

            return View();
        }

        public IActionResult Departments()
        {
            ViewData["Message"] = "Your Departments";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            return LocalRedirect(returnUrl);
        }
    }
}

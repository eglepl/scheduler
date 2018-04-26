using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Scheduler.Data;
using Scheduler.Models;
using Scheduler.Services;

namespace Scheduler.Controllers.WebApi
{
    [Route("api/[controller]/[action]")]
    public class CronWebApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<CronWebApiController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;
        private readonly IEmailSender _emailSender;

        private readonly static int HOURS_BEFORE_VISIT_NOTIFICATION = 24; 



        //private readonly ILogger _logger;


        public CronWebApiController(ApplicationDbContext context, IStringLocalizer<CronWebApiController> localizer, UserManager<ApplicationUser> userManager, IHostingEnvironment environment, IEmailSender emailSender/*, ILogger logger*/)
        {
            _context = context;
            _localizer = localizer;
            _userManager = userManager;
            _environment = environment;
            _emailSender = emailSender;
            //_logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> VisitNotifications()
        {
            var dateTimeNow = DateTime.Now;
            var visits = _context.Visits
                .Include(v => v.Patient)
                .Include(v => v.Doctor)
                .Where(v => 
                    v.VisitDateTime > dateTimeNow 
                    && dateTimeNow > v.VisitDateTime.AddHours(-(HOURS_BEFORE_VISIT_NOTIFICATION)) 
                    && v.IsNotified == false);

            var messageTemplate = ""
                    + "<p>Dear {0},</p>"
                    + "<p> You have an appointment with a Dr. {1} on {2:yyyy-MM-dd HH:mm (dddd)} "
                    + "at our Medical Center Clinic.</p>"
                    + "<p> Doctor contacts: <br>"
                    + "E-mail: <a href=\"mailto:{3}\">{3}</a><br>"
                    + "Phone Number: {4}"
                    + "</p>"
                    + "<p>You can cancel your appointment using "
                    + "this <a href=\"{5}\"> link</a >.</p> ";

            var count = 0;
            foreach (var visit in visits)
            {
                var subject = "Doctor Visit Notification, " + visit.Doctor.FullName;

                var message = String.Format(messageTemplate, new object[] {
                    visit.Patient.FullName,
                    visit.Doctor.FullName,
                    visit.VisitDateTime,
                    visit.Doctor.EMail,
                    visit.Doctor.PhoneNumber,
                    Url.Action("Doctors", "User")
                });
                await _emailSender.SendEmailAsync(visit.Patient.EMail, subject,
                    message);
                visit.IsNotified = true;
                count++;
            }
            _context.SaveChanges();

            return Ok(new {
                CountSent = count
            });
        }


    }
}
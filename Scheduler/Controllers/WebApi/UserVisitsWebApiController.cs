using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Scheduler.Data;
using Scheduler.Models;
using Scheduler.Models.HealthCenter;
using Scheduler.ViewModels.HealthCenter;

namespace Scheduler.Controllers.WebApi
{
    [Route("api/[controller]/[action]")]
    public class UserVisitsWebApiController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<UserVisitsWebApiController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;

        public UserVisitsWebApiController(ApplicationDbContext context, IStringLocalizer<UserVisitsWebApiController> localizer, UserManager<ApplicationUser> userManager, IHostingEnvironment environment/*, ILogger logger*/)
        {
            _context = context;
            _localizer = localizer;
            _userManager = userManager;
            _environment = environment;
            //_logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(VisitViewModel visitViewModel)
        {
            if (!User.Identity.IsAuthenticated) {
                return NotFound();
            }

            var errors = new List<String>();

            var user = await GetCurrentUserAsync();
            user = await _context.Users.Include(u => u.Patients).FirstOrDefaultAsync(u => u.Id == user.Id);

            // check doctor
            var doctor = await _context.Doctors.SingleOrDefaultAsync(m => m.ID == visitViewModel.DoctorID);
            if (doctor == null)
            {
                errors.Add("Selected Doctor does not exit");
            }

            // check patient
            var patient = await _context.Patients
                .Include(p => p.Bans)
                .Where(p => user.Patients.Contains(p))
                .SingleOrDefaultAsync(m => m.ID == visitViewModel.PatientID);
            if (patient == null)
            {
                errors.Add("Patient does not exist or does not belong to current user");
            }

            var bannedUntil = patient.Bans.Select(b => b.BannedUntil).DefaultIfEmpty(new DateTime(0)).Max();
            if (bannedUntil > DateTime.Now) {
                errors.Add(
                    "I am sorry, the patient is banned until "
                    + String.Format("{0:yyyy-MM-dd HH:mm}", bannedUntil)
                    + " for not attending doctor visits"
                );
            }

            // check visit date
            if (ModelState.IsValid && errors.Count() == 0)
            {
                var time = _context.DoctorScheduleFutureTimes.SingleOrDefault(ft => ft.VisitDateTime == visitViewModel.VisitDateTime && ft.DoctorID == doctor.ID);

                if (time == null)
                {
                    errors.Add("No doctor schedule appointment time exists with provided date time.");
                }
                else
                {
                    var usedVisit = _context.Visits.SingleOrDefault(v => v.VisitDateTime == visitViewModel.VisitDateTime && v.DoctorID == doctor.ID);

                    if (usedVisit == null)
                    {
                        _context.Visits.Add(new Visit
                        {
                            DoctorID = visitViewModel.DoctorID,
                            PatientID = visitViewModel.PatientID,
                            VisitDateTime = visitViewModel.VisitDateTime,
                            PatientNotes = visitViewModel.PatientNotes
                        });
                        _context.SaveChanges();
                        return Ok(new
                        {
                            success = true,
                            successMessage = "Visit successfully created"
                        });
                    }
                    else
                    {
                        errors.Add("Doctor appointment time is already taken. Please select another one.");
                    }
                }
            }
            else
            {
                errors.Add("Incorrect or already occupied appointment date time");
            }

            return BadRequest(new {
                success = false,
                errorMessage = String.Join("<br >", errors)
                });
        }

        public async Task<object> Visits(DataSourceLoadOptions loadOptions)
        {
            var user = await GetCurrentUserAsync();
            var patientIDList = _context.Patients.Where(p => p.ApplicationUserID == user.Id).Select(p => p.ID);

            var visits = _context.Visits.Include(v => v.Patient).Include(v => v.Doctor).Where(v => patientIDList.Contains(v.PatientID)).Select(v => new UserVisitViewModel
            {
                ID = v.ID,
                PatientFullName = v.Patient.FirstName + " " + v.Patient.LastName,
                DoctorFullName = v.Doctor.FirstName + " " + v.Doctor.LastName,
                PatientID  = v.PatientID,
                DoctorID = v.DoctorID,
                VisitDateTime = v.VisitDateTime
            });
            return DataSourceLoader.Load(visits, loadOptions);
        }

        [HttpDelete]
        public async Task<IActionResult> VisitDelete(int key)
        {
            var user = await GetCurrentUserAsync();
            var visit = _context.Visits.Include(v => v.Patient).ThenInclude(p => p.ApplicationUser).FirstOrDefault(v => v.ID == key && v.Patient.ApplicationUserID == user.Id && v.VisitDateTime > DateTime.Now);
            if (visit == null)
            {
                return NotFound();
            }
            _context.Visits.Remove(visit);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
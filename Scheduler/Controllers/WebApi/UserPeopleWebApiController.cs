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
using Scheduler.ViewModels.HealthCenter;

namespace Scheduler.Controllers.WebApi
{
    [Route("api/[controller]/[action]")]
    public class UserPeopleWebApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<UserPeopleWebApiController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;

        //private readonly ILogger _logger;


        public UserPeopleWebApiController(ApplicationDbContext context, IStringLocalizer<UserPeopleWebApiController> localizer, UserManager<ApplicationUser> userManager, IHostingEnvironment environment/*, ILogger logger*/)
        {
            _context = context;
            _localizer = localizer;
            _userManager = userManager;
            _environment = environment;
            //_logger = logger;
        }

        public async Task<object> People(DataSourceLoadOptions loadOptions)
        {
            var user = await GetCurrentUserAsync();
            var patients = _context.Patients.Include(p => p.Bans).Where(p => p.ApplicationUserID == user.Id).Select(p => new PatientViewModel {
                ID = p.ID,
                FirstName = p.FirstName,
                LastName = p.LastName,
                BirthDate = p.BirthDate,
                PhoneNumber = p.PhoneNumber,
                EMail = p.EMail,
                Address = p.Address,
                City = p.City,
                Sex = p.Sex,
                Notes = p.Notes,
                PersonalCode = p.PersonalCode,
                IsBanned = p.Bans.Select(b => b.BannedUntil).DefaultIfEmpty(new DateTime(0)).Max() > DateTime.Now
            });
            return DataSourceLoader.Load(patients, loadOptions);
        }

        
        [HttpPost]
        [Route("{patientID:int}")]
        public async Task<IActionResult> PatientDelete([FromRoute] int patientID)
        {
            var id = patientID;
            var currentUser = await GetCurrentUserAsync();
            var patient = await _context.Patients.Include(p => p.Visits).SingleOrDefaultAsync(m => m.ID == id && m.ApplicationUserID == currentUser.Id);

            if (patient == null)
            {
                return Ok(new {
                    success = false,
                    errorMessage = "Could not delete provided patient. Patient does not belong to you or does not exist."
                });
            }
            var visit = _context.Visits.Include(v => v.Patient).FirstOrDefault(v => v.PatientID == id && v.VisitDateTime > DateTime.Now);

            if (visit == null)
            {
                patient.Visits.Clear();
                //var visitsToDelete = _context.Visits.Where(v => v.PatientID == patientID).ToList();

                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
                return Ok(new {
                    success = true,
                    successMessage = "Patient deleted successfully."
                });

            }
            return Ok(new
            {
                success = false,
                errorMessage = "Please cancel all patient's visits before deleting."
            });

        }

        public IActionResult Index()
        {
            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
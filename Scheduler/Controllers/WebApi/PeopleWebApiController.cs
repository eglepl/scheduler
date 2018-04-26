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
using Newtonsoft.Json;
using Scheduler.Data;
using Scheduler.Extensions;
using Scheduler.Models;
using Scheduler.Models.HealthCenter;
using Scheduler.ViewModels.HealthCenter;

namespace Scheduler.Controllers.WebApi
{
    [Route("api/[controller]/[action]")]
    public class PeopleWebApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<PeopleWebApiController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;

        //private readonly ILogger _logger;


        public PeopleWebApiController(ApplicationDbContext context, IStringLocalizer<PeopleWebApiController> localizer, UserManager<ApplicationUser> userManager, IHostingEnvironment environment/*, ILogger logger*/)
        {
            _context = context;
            _localizer = localizer;
            _userManager = userManager;
            _environment = environment;
            //_logger = logger;
        }

        public object People(DataSourceLoadOptions loadOptions)
        {
            var patients = _context.Patients.Select(p => new PatientViewModel {
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
                PersonalCode = p.PersonalCode
            });
            return DataSourceLoader.Load(patients, loadOptions);
        }

        
        [HttpPost]
        [Route("{patientID:int}")]
        public async Task<IActionResult> PatientDelete([FromRoute] int patientID)
        {
            var id = patientID;
            var patient = await _context.Patients.Include(p => p.Visits).SingleOrDefaultAsync(m => m.ID == id);

            if (patient == null)
            {
                return Ok(new {
                    success = false,
                    errorMessage = "Could not delete provided patient. Patient does not exist."
                });
            }
            var visit = _context.Visits.Include(v => v.Patient).FirstOrDefault(v => v.PatientID == id && v.VisitDateTime > DateTime.Now);

            if (visit == null)
            {
                patient.Visits.Clear();

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

        [Route("{patientID:int}")]
        public object PatientBans([FromRoute] int patientID, DataSourceLoadOptions loadOptions)
        {
            var patientBans = _context.PatientBans
                .Include(pb => pb.Patient)
                .Where(pb => pb.PatientID == patientID)
                .Select(pb => new PatientBanViewModel
            {
                ID = pb.ID,
                BannedUntil = pb.BannedUntil,
                PatientID = pb.PatientID,
                PatientFullName = pb.Patient.FullName
            });
            return DataSourceLoader.Load(patientBans, loadOptions);
        }


        [Route("{patientID:int}")]
        public object PatientBanCreate([FromRoute] int patientID, string values)
        {
            try
            {
                var patient = _context.Patients.Include(p => p.Bans).SingleOrDefault(m => m.ID == patientID);

                if (patient == null)
                {
                    return NotFound();
                }

                var patientBanViewModel = new PatientBanViewModel();
                JsonConvert.PopulateObject(values, patientBanViewModel);

                if (!TryValidateModel(patientBanViewModel))
                    return BadRequest(ModelState.GetFullErrorMessage());

                var patientBan = new PatientBan()
                {
                    BannedUntil = patientBanViewModel.BannedUntil,
                    PatientID = patientID,
                };

                patient.Bans.Add(patientBan);

                _context.PatientBans.Add(patientBan);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{patientID:int}")]
        public IActionResult PatientBanDelete([FromRoute] int patientID, int key)
        {
            var patientBan = _context.PatientBans.SingleOrDefault(pb => pb.ID == key && pb.PatientID == patientID);
            if (patientBan == null)
            {
                return NotFound();
            }

            _context.PatientBans.Remove(patientBan);
            _context.SaveChanges();

            return Ok();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}
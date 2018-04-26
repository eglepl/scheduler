using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Scheduler.Data;
using Scheduler.Extensions;
using Scheduler.Models;
using Scheduler.Models.HealthCenter;
using Scheduler.ViewModels.HealthCenter;

namespace Scheduler.Controllers.HealthCenter
{
    [Route("api/[controller]/[action]")]
    public class AdminWebApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<AdminWebApiController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;


        public AdminWebApiController(ApplicationDbContext context, IStringLocalizer<AdminWebApiController> localizer, UserManager<ApplicationUser> userManager, ILogger<AdminWebApiController> logger)
        {
            _context = context;
            _localizer = localizer;
            _userManager = userManager;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public object DoctorsList(DataSourceLoadOptions loadOptions)
        {
            var doctors = _context.Doctors.Include(d => d.DoctorScheduleTimes).Select(d => new DoctorViewModel
            {
                ID = d.ID,
                FirstName = d.FirstName,
                LastName = d.LastName,
                BirthDate = d.BirthDate,
                PhoneNumber = d.PhoneNumber,
                EMail = d.EMail,
                Address = d.Address,
                City = d.City,
                Sex = d.Sex,
                Notes = d.Notes,
                PersonalCode = d.PersonalCode,
                IsDisabled = d.IsDisabled,
                FreeVisitsCount = d.DoctorScheduleTimes.Where(dst => dst.VisitDateTime > DateTime.Now && !_context.Visits.Select(v => v.VisitDateTime).Contains(dst.VisitDateTime)).Count()
            });
            return DataSourceLoader.Load(doctors, loadOptions);
        }

        [HttpGet]
        public object PatientsList(DataSourceLoadOptions loadOptions)
        {
            // Magiskas Query Paciento su jo lankymosi po paskutinio baninimo statistika.
            var patients = _context.ExecRawSQL<AdminPatientViewModel>(@"
                SELECT PP.*, C.AllVisitsCount, C.NotArrivedVisitsCount
                FROM [People] AS PP
                LEFT JOIN (SELECT P.ID, count(V.ID) as AllVisitsCount, SUM(CASE WHEN V.HasArrived=0 THEN 1 ELSE 0 END) as NotArrivedVisitsCount
                  FROM [People] AS P
                  LEFT JOIN [Visits] as V on P.ID = V.PatientID AND V.VisitDateTime < @todatetime AND V.VisitDateTime > (SELECT ISNULL(MAX(B.BannedUntil), DATEFROMPARTS(1,1,1)) FROM PatientBans as B where B.PatientID = P.ID)
                  group by P.ID, P.FirstName, P.LastName) AS C ON C.ID = PP.ID
                WHERE PP.Discriminator = 'Patient';
                ", new SqlParameter[] {
                    new SqlParameter("todatetime", DateTime.Now)
            });

        //var patients = _context.Patients
        //        .Include(p => p.Visits)
        //        .Include(p => p.Bans)
        //        .Select(p => new AdminPatientViewModel
        //    {
        //        ID = p.ID,
        //        FirstName = p.FirstName,
        //        LastName = p.LastName,
        //        BirthDate = p.BirthDate,
        //        PhoneNumber = p.PhoneNumber,
        //        EMail = p.EMail,
        //        Address = p.Address,
        //        City = p.City,
        //        Sex = p.Sex,
        //        Notes = p.Notes,
        //        PersonalCode = p.PersonalCode,
        //        AllVisitsCount = p.Visits
        //            .Where(
        //                v => v.VisitDateTime <= DateTime.Now
        //                && v.VisitDateTime > (p.Bans.Count() > 0 ? p.Bans.Max(b => b.BannedUntil) : new DateTime(0))).Count(),
        //            NotArrivedVisitsCount = p.Visits
        //            .Where(
        //                v => v.HasArrived == false
        //                && v.VisitDateTime <= DateTime.Now
        //                && v.VisitDateTime > (p.Bans.Count() > 0 ? p.Bans.Max(b => b.BannedUntil) : new DateTime(0))).Count()
        //        });
            return DataSourceLoader.Load(patients, loadOptions);
        }

        [HttpGet]
        public object VisitsList(int id, DataSourceLoadOptions loadOptions)
        {
            _logger.LogInformation("LOG: VISITSLIST ID " + id);
            var visits = _context.Visits
                .Include(v => v.Doctor)
                .Include(v => v.Patient)
                .Where(v => v.PatientID == id)
                .Select(v => new AdminUserVisitsViewModel
                {
                    ID = v.ID,
                    PatientID = v.PatientID,
                    VisitDateTime = v.VisitDateTime,
                    DoctorID = v.DoctorID,
                    DoctorNotes = v.DoctorNotes,
                    PatientNotes = v.PatientNotes,
                    DoctorFullName = v.Doctor.FullName,
                    DoctorPosition = v.Doctor.Position,
                    PatientFullName = v.Patient.FullName,
                    HasArrived = v.HasArrived
                });

            _logger.LogInformation("LOG: VISITSLIST COUNT " + visits.Count().ToString());
            return DataSourceLoader.Load(visits, loadOptions);
        }

        [HttpPut]
        public IActionResult VisitUpdate(int key, string values)
        {
            var adminUserVisitsViewModel = new AdminUserVisitsViewModel();
            JsonConvert.PopulateObject(values, adminUserVisitsViewModel);

            var visit = _context.Visits.FirstOrDefault(v => v.ID == key);

            if (visit == null)
            {
                return NotFound("Visit Not Found");
            }

            visit.HasArrived = adminUserVisitsViewModel.HasArrived;

            _context.SaveChanges();

            return Ok();
        }

        public IActionResult PatientBan()
        {
            return Ok();
        }

        
    }
}
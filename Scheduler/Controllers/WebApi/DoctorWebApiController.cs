using System;
using System.Collections.Generic;
using System.Data.Common;
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
    public class DoctorWebApiController : Controller
    {private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<DoctorWebApiController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;

        //private readonly ILogger _logger;


        public DoctorWebApiController(ApplicationDbContext context, IStringLocalizer<DoctorWebApiController> localizer, UserManager<ApplicationUser> userManager, IHostingEnvironment environment/*, ILogger logger*/)
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

        [Route("{doctorID:int}/{month:DateTime}")]
        [HttpGet]
        public object Schedule([FromRoute]int doctorID, [FromRoute]DateTime month, DataSourceLoadOptions loadOptions)
        {
            month = new DateTime(month.Year, month.Month, 1, 0, 0, 0);

            var doctorSchedules = _context.DoctorSchedules
                .Where(ds => ds.DoctorID == doctorID && ds.VisitDate.Month == month.Month && ds.VisitDate.Year == month.Year);

            var visitTemplates = new List<DoctorScheduleViewModel>();
            var currentDate = month;
            while (currentDate.Month == month.Month)
            {
                var doctorScheduleViewModel = new DoctorScheduleViewModel();
                doctorScheduleViewModel.VisitDate = currentDate;
                doctorScheduleViewModel.VisitScheduleTemplateID = null;
                var doctorSchedule = doctorSchedules.FirstOrDefault(d => d.VisitDate == currentDate);
                if (doctorSchedule != null )
                {
                    doctorScheduleViewModel.VisitScheduleTemplateID = doctorSchedule.VisitScheduleTemplateID;
                }
                visitTemplates.Add(doctorScheduleViewModel);
                currentDate = currentDate.AddDays(1);
            }



            return DataSourceLoader.Load(visitTemplates, loadOptions);
        }


        [Route("{doctorID:int}")]
        [HttpPut]
        public IActionResult ScheduleUpdate([FromRoute]int doctorID, DateTime key, string values)
        {
            try
            {
                key = new DateTime(key.Year, key.Month, key.Day, 0, 0, 0);
                var doctorScheduleViewModel = new DoctorScheduleViewModel();
                JsonConvert.PopulateObject(values, doctorScheduleViewModel);

                if (!TryValidateModel(doctorScheduleViewModel))
                    return BadRequest(ModelState.GetFullErrorMessage());

                var doctor = _context.Doctors.FirstOrDefault(d => d.ID == doctorID);
                if (doctor == null)
                {
                    return BadRequest("Doctor Not Found");
                }


                var template = _context.VisitScheduleTemplates.FirstOrDefault(t => t.ID == doctorScheduleViewModel.VisitScheduleTemplateID);

                if (template == null)
                {
                    return BadRequest("Template does not exist");
                }

                var doctorSchedule = _context.DoctorSchedules.FirstOrDefault(f => f.DoctorID == doctorID && f.VisitDate == key);

                if (doctorSchedule == null)
                {
                    
                    doctorSchedule = new DoctorSchedule();
                    doctorSchedule.VisitDate = key;
                    doctorSchedule.Doctor = doctor;
                    doctorSchedule.VisitScheduleTemplate = template;
                    _context.DoctorSchedules.Add(doctorSchedule);
                }
                else
                {
                    doctorSchedule.VisitScheduleTemplate = template;
                }

                _context.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("{doctorID:int}")]
        public object FreeVisitTimes([FromRoute]int doctorID, DataSourceLoadOptions loadOptions)
        {
            var freeVisitTimes = _context.DoctorScheduleFutureTimes.Where(t => t.DoctorID == doctorID && t.VisitDateTime > DateTime.Now && !_context.Visits.Select(v => v.VisitDateTime).Contains(t.VisitDateTime));

            return DataSourceLoader.Load(freeVisitTimes, loadOptions);
        }
    }
}
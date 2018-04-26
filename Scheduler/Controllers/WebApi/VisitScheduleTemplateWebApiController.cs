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
    public class VisitScheduleTemplateWebApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<VisitScheduleTemplateWebApiController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;

        //private readonly ILogger _logger;


        public VisitScheduleTemplateWebApiController(ApplicationDbContext context, IStringLocalizer<VisitScheduleTemplateWebApiController> localizer, UserManager<ApplicationUser> userManager, IHostingEnvironment environment/*, ILogger logger*/)
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

        [HttpGet]
        public object All(DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_context.VisitScheduleTemplates.Select(v => new VisitScheduleTemplateViewModel {
                   ID = v.ID,
                   Title = v.Title
            }), loadOptions);
        }

        [Route("{templateID:int}")]
        [HttpGet]
        public object TemplateTimes([FromRoute]int templateID, DataSourceLoadOptions loadOptions)
        {
            return DataSourceLoader.Load(_context.VisitScheduleTemplateTimes.Where(t => t.VisitScheduleTemplateID == templateID).Select(t => new VisitScheduleTemplateTimeViewModel
            {
                ID = t.ID,
                Start = t.Start,
            }), loadOptions);
        }

        [Route("{templateID:int}")]
        [HttpPost]
        public IActionResult TemplateTimeInsert([FromRoute]int templateID, string values)
        {
            try
            {
                var visitScheduleTemplate = _context.VisitScheduleTemplates.Include(t => t.Times).SingleOrDefault(t => t.ID == templateID);

                if (visitScheduleTemplate == null)
                {
                    return NotFound();
                }

                var visitScheduleTemplateTimeViewModel = new VisitScheduleTemplateTimeViewModel();
                JsonConvert.PopulateObject(values, visitScheduleTemplateTimeViewModel);

                if (!TryValidateModel(visitScheduleTemplateTimeViewModel))
                    return BadRequest(ModelState.GetFullErrorMessage());

                var visitScheduleTemplateTime = new VisitScheduleTemplateTime()
                {
                    Start = visitScheduleTemplateTimeViewModel.Start,
                };

                visitScheduleTemplate.Times.Add(visitScheduleTemplateTime);

                _context.VisitScheduleTemplateTimes.Add(visitScheduleTemplateTime);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }

        [Route("{templateID:int}")]
        [HttpDelete]
        public IActionResult TemplateTimeDelete([FromRoute]int templateID, int key)
        {
            var templateTime = _context.VisitScheduleTemplateTimes.FirstOrDefault(t => t.VisitScheduleTemplateID == templateID && t.ID == key);
            if (templateTime == null)
            {
                return NotFound();
            }
            _context.VisitScheduleTemplateTimes.Remove(templateTime);
            _context.SaveChanges();
            return Ok();
        }

        [Route("{templateID:int}")]
        [HttpPost]
        public IActionResult TemplateDelete([FromRoute]int templateID)
        {
            var id = templateID;
            var doctorSchedule = _context.DoctorSchedules.Include(ds => ds.VisitScheduleTemplate).FirstOrDefault(ds => ds.VisitScheduleTemplateID == id );

            if(doctorSchedule != null)
            {
                return Ok(new
                {
                    success = false,
                    errorMessage = "Can not delete template. It is used in doctor schedule."
                });
            }

            var template = _context.VisitScheduleTemplates.FirstOrDefault(t => t.ID == id);

            if (template == null)
            {
                return Ok(new
                {
                    success = false,
                    erorrMessage = "Can not delete template. It does not exist."
                });
            }

            _context.VisitScheduleTemplates.Remove(template);
            _context.SaveChangesAsync();
            return Ok(new
            {
                success = true,
                successMessage = "Template sucessfully deleted."
            });
        }

    }
}
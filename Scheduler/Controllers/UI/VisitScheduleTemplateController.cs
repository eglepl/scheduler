using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Scheduler.Data;
using Scheduler.Models;
using Scheduler.Models.HealthCenter;
using Scheduler.ViewModels.HealthCenter;

namespace Scheduler.Controllers.UI
{
    public class VisitScheduleTemplateController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<VisitScheduleTemplateController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;

        //private readonly ILogger _logger;


        public VisitScheduleTemplateController(ApplicationDbContext context, IStringLocalizer<VisitScheduleTemplateController> localizer, UserManager<ApplicationUser> userManager, IHostingEnvironment environment/*, ILogger logger*/)
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

        public IActionResult All()
        {
            return View();
        }

        //Atvaizduoti grid visu templatu
        //Sukurti vizito templato redagavimo forma su vizito laikais(grid crud no edit)

        public IActionResult Create()
        {
            var visitScheduleTemplateViewModel = new VisitScheduleTemplateViewModel();
            return View(visitScheduleTemplateViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VisitScheduleTemplateViewModel visitScheduleTemplateViewModel)
        {
            if (ModelState.IsValid)
            {

                var visitScheduleTemplate = new VisitScheduleTemplate()
                {
                    Title = visitScheduleTemplateViewModel.Title
                };


                _context.VisitScheduleTemplates.Add(visitScheduleTemplate);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(VisitScheduleTemplateController.Edit), "VisitScheduleTemplate", new { id = visitScheduleTemplate.ID, notification = "successfully-created" });

            }
            return View(visitScheduleTemplateViewModel);
        }

        public async Task<IActionResult> Edit(int id, string notification ="")
        {
            if (notification.Equals("successfully-created"))
            {
                var message = "Successfully created Template. Now you can add visit times.";
                ViewData["SuccessMessage"] = message;
            }
            var visitScheduleTemplate = await _context.VisitScheduleTemplates.SingleOrDefaultAsync(m => m.ID == id);
            if (visitScheduleTemplate == null)
            {
                return NotFound();
            }

            var visitScheduleTemplateViewModel = new VisitScheduleTemplateViewModel()
            {
                ID = visitScheduleTemplate.ID,
                Title = visitScheduleTemplate.Title,
            };

            return View(visitScheduleTemplateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(VisitScheduleTemplateViewModel visitScheduleTemplateViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var visitScheduleTemplate = await _context.VisitScheduleTemplates.SingleOrDefaultAsync(m => m.ID == visitScheduleTemplateViewModel.ID);
                    if (visitScheduleTemplate == null)
                    {
                        return NotFound();
                    }

                    UpdateVisitScheduleTemplate(visitScheduleTemplate, visitScheduleTemplateViewModel);

                    _context.Update(visitScheduleTemplate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                ViewData["SuccessMessage"] = "Successfully saved template name.";
                ViewData["WarningMessage"] = "Do not forget to add template times in the grid.";
                return View(visitScheduleTemplateViewModel);
            }
            return View(visitScheduleTemplateViewModel);
        }

      

        private void UpdateVisitScheduleTemplate(VisitScheduleTemplate visitScheduleTemplate, VisitScheduleTemplateViewModel visitScheduleTemplateViewModel)
        {
            visitScheduleTemplate.Title = visitScheduleTemplateViewModel.Title;

        }
    }
}
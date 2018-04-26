using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Scheduler.Data;
using Scheduler.Models;
using Scheduler.ViewModels.HealthCenter;
using Scheduler.Models.HealthCenter;


namespace Scheduler.Controllers
{
    public class PeopleController : Controller
    {


        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<PeopleController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly ILogger _logger;


        public PeopleController(ApplicationDbContext context, IStringLocalizer<PeopleController> localizer, UserManager<ApplicationUser> userManager/*, ILogger logger*/)
        {
            _context = context;
            _localizer = localizer;
            _userManager = userManager;
            //_logger = logger;
        }
        
        public IActionResult People()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("People") });
            }
            return View();
        }

        //public IActionResult Create()
        //{
        //    var patientViewModel = new PatientViewModel();
        //    return View(patientViewModel);
        //}


        //[HttpPost]
        //public async Task<IActionResult> Create(PatientViewModel patientViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var currentUser = await GetCurrentUserAsync();
        //        var patient = new Patient() {
        //            ApplicationUserID = currentUser.Id,
        //            FirstName = patientViewModel.FirstName,
        //            LastName = patientViewModel.LastName,
        //            PersonalCode = patientViewModel.PersonalCode,
        //            Address = patientViewModel.Address,
        //            BirthDate = patientViewModel.BirthDate,
        //            City = patientViewModel.City,
        //            PhoneNumber = patientViewModel.PhoneNumber,
        //            EMail = patientViewModel.EMail,
        //            Notes = patientViewModel.Notes,
        //            Sex = patientViewModel.Sex
        //        };
        //        patient.Avatar = null;

        //        _context.Patients.Add(patient);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(UserPeopleController.Edit), "UserPeople", new { id = patient.ID, notification = "successfuly-created"});

        //    }
        //    return View(patientViewModel);
        //}

        public async Task<IActionResult> Details(int id)
        {

            var patient = await _context.Patients.SingleOrDefaultAsync(m => m.ID == id);

            if (patient == null)
            {
                return NotFound();
            }

            var patientViewModel = new PatientViewModel()
            {
                ID = patient.ID,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                BirthDate = patient.BirthDate,
                Sex = patient.Sex,
                PersonalCode = patient.PersonalCode,
                PhoneNumber = patient.PhoneNumber,
                Address = patient.Address,
                City = patient.City,
                EMail = patient.EMail,
                Notes = patient.Notes
            };

            return View(patientViewModel);
        }

        public async Task<IActionResult> Edit(int? id, string notification = "")
        {
            if (notification.Equals("successfuly-created"))
            {
                var message = "Patient is successfully saved. Edit your profile.";
                ViewData["SuccessMessageCreatedPerson"] = message;
            }
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients.Include(d => d.Avatar).SingleOrDefaultAsync(m => m.ID == id);
            if (patient == null)
            {
                return NotFound();
            }

            var patientViewModel = new PatientViewModel()
            {
                ID = patient.ID,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Sex = patient.Sex,
                BirthDate = patient.BirthDate,
                PersonalCode = patient.PersonalCode,
                Address = patient.Address,
                City = patient.City,
                EMail = patient.EMail,
                Notes = patient.Notes,
                PhoneNumber = patient.PhoneNumber
            };

            return View(patientViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PatientViewModel patientViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var patient = await _context.Patients.Include(d => d.Avatar).SingleOrDefaultAsync(m => m.ID == patientViewModel.ID);

                    if (patient == null)
                    {
                        return NotFound();
                    }

                    UpdatePatient(patient, patientViewModel);

                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }


                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                ViewData["SuccessMessage"] = "Successfully Saved";
                return View(patientViewModel);
            }
            return View(patientViewModel);
        }

        public async Task<IActionResult> Avatar(int id)
        {
            var patient = await _context.Patients.Include(d => d.Avatar).FirstOrDefaultAsync(t => t.ID == id);
            if (patient == null)
            {
                return NotFound();
            }
            var avatar = patient.Avatar;
            if (avatar == null)
            {
                return File("~/images/avatar-empty.png", "image/png");
            }
            return File(avatar.Content, avatar.ContentType);
        }

        [HttpPost]
        public async Task<IActionResult> AvatarUpload(int id, AvatarUploadViewModel avatarViewModel)
        {
            if (ModelState.IsValid)
            {
                var patient = await _context.Patients.Include(d => d.Avatar).FirstOrDefaultAsync(t => t.ID == id);
                if (patient == null)
                {
                    return NotFound("Doctor not found with provided ID");
                }
                if (patient.Avatar == null)
                {
                    patient.Avatar = new File(avatarViewModel.AvatarFile);
                    _context.Files.Add(patient.Avatar);
                }
                else
                {
                    patient.Avatar.Load(avatarViewModel.AvatarFile);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("Model not valid");
        }



        private void UpdatePatient(Patient patient, PatientViewModel patientViewModel)
        {
            patient.FirstName = patientViewModel.FirstName;
            patient.LastName = patientViewModel.LastName;
            patient.Sex = patientViewModel.Sex;
            patient.BirthDate = patientViewModel.BirthDate;
            patient.PersonalCode = patientViewModel.PersonalCode;
            patient.PhoneNumber = patientViewModel.PhoneNumber;
            patient.EMail = patientViewModel.EMail;
            patient.Notes = patientViewModel.Notes;
            patient.Address = patientViewModel.Address;
            patient.City = patientViewModel.City;
        }

  

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
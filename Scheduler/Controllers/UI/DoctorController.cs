using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class DoctorController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<DoctorController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;

        //private readonly ILogger _logger;


        public DoctorController(ApplicationDbContext context, IStringLocalizer<DoctorController> localizer, UserManager<ApplicationUser> userManager, IHostingEnvironment environment/*, ILogger logger*/)
        {
            _context = context;
            _localizer = localizer;
            _userManager = userManager;
            _environment = environment;
            //_logger = logger;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var doctorViewModel = new DoctorViewModel();
            return View(doctorViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(DoctorViewModel doctorViewModel)
        {
            if (ModelState.IsValid)
            {

                var doctor = new Doctor();
                UpdateDoctor(doctor, doctorViewModel);
                doctor.Avatar = null;

                _context.Doctors.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(DoctorController.Edit), "Doctor", new { id = doctor.ID, notification = "successfully-created" });

            }
            return View(doctorViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id, string notification = "")
        {
            if (notification.Equals("successfully-created"))
            {
                var message = "Patient is successfully saved. Edit your profile.";
                ViewData["SuccessMessage"] = message;
            }
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctors.Include(d => d.Avatar).SingleOrDefaultAsync(m => m.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            var doctorViewModel = new DoctorViewModel();
            UpdateDoctorViewModel(doctor, doctorViewModel);


            return View(doctorViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DoctorViewModel doctorViewModel, string subaction)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var doctor = await _context.Doctors.Include(d => d.Avatar).SingleOrDefaultAsync(m => m.ID == doctorViewModel.ID);
                    if (doctor == null)
                    {
                        return NotFound();
                    }

                        UpdateDoctor(doctor, doctorViewModel);

                        _context.Update(doctor);
                        await _context.SaveChangesAsync();
                        }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                ViewData["SuccessMessage"] = "Successfully Saved"; 
                return View(doctorViewModel);
            }
            return View(doctorViewModel);
        }
        [Authorize(Roles = "Admin, Patient")]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var doctor = await _context.Doctors.Include(d => d.Avatar).SingleOrDefaultAsync(m => m.ID == id);

            if (doctor == null)
            {
                return NotFound();
            }

            var doctorViewModel = new DoctorViewModel();
            UpdateDoctorViewModel(doctor, doctorViewModel);
      

            return View(doctorViewModel);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var doctor = await _context.Doctors.SingleOrDefaultAsync(m => m.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }

            var doctorViewModel = new DoctorViewModel();
            UpdateDoctorViewModel(doctor, doctorViewModel);

            return View(doctorViewModel);
        }

        [Authorize(Roles = "Admin")]
        // POST: Doctors/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DoctorViewModel doctorViewModel)
        {
            var doctor = await _context.Doctors.SingleOrDefaultAsync(m => m.ID == doctorViewModel.ID);
            if (doctor == null)
            {
                return NotFound();
            }
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AdminController.DoctorsList), "Admin");
        }
        
        public async Task<IActionResult> Avatar(int id)
        {
            var doctor = await _context.Doctors.Include(d => d.Avatar).FirstOrDefaultAsync(t => t.ID == id);
            if (doctor == null)
            {
                return NotFound();
            }
            var avatar = doctor.Avatar;
            if (avatar == null)
            {
                return File("~/images/avatar-empty.png", "image/png");
            }
            return File(avatar.Content, avatar.ContentType);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AvatarUpload(int id, AvatarUploadViewModel avatarViewModel)
        {
            if (ModelState.IsValid) {
                var doctor = await _context.Doctors.Include(d => d.Avatar).FirstOrDefaultAsync(t => t.ID == id);
                if (doctor == null)
                {
                    return NotFound("Doctor not found with provided ID");
                }
                if (doctor.Avatar == null)
                {
                    doctor.Avatar = new Models.HealthCenter.File(avatarViewModel.AvatarFile);
                    _context.Files.Add(doctor.Avatar);
                }
                else
                {
                    doctor.Avatar.Load(avatarViewModel.AvatarFile);
                }
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("Model not valid");
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        private void UpdateDoctor(Doctor doctor, DoctorViewModel doctorViewModel)
        {
            doctor.FirstName = doctorViewModel.FirstName;
            doctor.LastName = doctorViewModel.LastName;
            doctor.Sex = doctorViewModel.Sex;
            doctor.BirthDate = doctorViewModel.BirthDate;
            doctor.PersonalCode = doctorViewModel.PersonalCode;
            doctor.PhoneNumber = doctorViewModel.PhoneNumber;
            doctor.EMail = doctorViewModel.EMail;
            doctor.Notes = doctorViewModel.Notes;
            doctor.Position = doctorViewModel.Position;
            doctor.Address = doctorViewModel.Address;
            doctor.City = doctorViewModel.City;
            doctor.IsDisabled = doctorViewModel.IsDisabled;
        }

        [Authorize(Roles = "Admin")]
        private void UpdateDoctorViewModel(Doctor doctor, DoctorViewModel doctorViewModel)
        {
            doctorViewModel.ID = doctor.ID;
            doctorViewModel.FirstName = doctor.FirstName;
            doctorViewModel.LastName = doctor.LastName;
            doctorViewModel.BirthDate = doctor.BirthDate;
            doctorViewModel.Sex = doctor.Sex;
            doctorViewModel.PersonalCode = doctor.PersonalCode;
            doctorViewModel.PhoneNumber = doctor.PhoneNumber;
            doctorViewModel.EMail = doctor.EMail;
            doctorViewModel.Notes = doctor.Notes;
            doctorViewModel.Position = doctor.Position;
            doctorViewModel.Address = doctor.Address;
            doctorViewModel.City = doctor.City;
            doctorViewModel.IsDisabled = doctor.IsDisabled;
        }
    }
}
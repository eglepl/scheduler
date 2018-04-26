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
    public class UserWebApiController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStringLocalizer<UserWebApiController> _localizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHostingEnvironment _environment;

        //private readonly ILogger _logger;


        public UserWebApiController(ApplicationDbContext context, IStringLocalizer<UserWebApiController> localizer, UserManager<ApplicationUser> userManager, IHostingEnvironment environment/*, ILogger logger*/)
        {
            _context = context;
            _localizer = localizer;
            _userManager = userManager;
            _environment = environment;
            //_logger = logger;
        }

        [HttpGet]
        public object Doctors(DataSourceLoadOptions loadOptions)
        {
            var doctors = _context.Doctors.Where(d => d.IsDisabled == false).Select(d => new DoctorViewModel
            {
                ID = d.ID,
                FirstName = d.FirstName,
                LastName = d.LastName,
                BirthDate = d.BirthDate,
                PhoneNumber = d.PhoneNumber,
                EMail = d.EMail,
                Address = d.Address,
                City = d.City,
                Position = d.Position,
                Sex = d.Sex,
                Notes = d.Notes,
                PersonalCode = d.PersonalCode,
                FreeVisitsCount = d.DoctorScheduleTimes.Where(dst => dst.VisitDateTime > DateTime.Now && !_context.Visits.Select(v => v.VisitDateTime).Contains(dst.VisitDateTime)).Count()
            });
            return DataSourceLoader.Load(doctors, loadOptions);
        }


        public object GetMessages(string userID, string chatUserID, DataSourceLoadOptions loadOptions)
        {
            var chats = _context.ChatMessages.Include(c => c.FromUser).Include(c => c.ToUser).Where(c => (c.ApplicationUserIDFrom == userID && c.ApplicationUserIDTo == chatUserID) || (c.ApplicationUserIDFrom == chatUserID && c.ApplicationUserIDTo == userID)).Select(c => new ChatMessageViewModel {
                ID = c.ID,
                ApplicationUserIDFrom = c.ApplicationUserIDFrom,
                ApplicationUserIDTo = c.ApplicationUserIDTo,
                From = c.FromUser.Email,
                To = c.ToUser.Email,
                MessageText = c.Message,
                MessageTime = c.MessageTime,
                Direction = (c.ApplicationUserIDFrom == userID) ? "sent" : "received"
            });

            return DataSourceLoader.Load(chats, loadOptions);
        }

        [HttpPost]
        public IActionResult ChatMessageSend(ChatMessageViewModel chatMessageViewModel) {
            if (ModelState.IsValid) {
                _context.ChatMessages.Add(new ChatMessage {
                    ApplicationUserIDFrom = chatMessageViewModel.ApplicationUserIDFrom,
                    ApplicationUserIDTo = chatMessageViewModel.ApplicationUserIDTo,
                    Message = chatMessageViewModel.MessageText,
                    MessageTime = DateTime.Now
                });
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        public async Task<object> GetChatParticipants(DataSourceLoadOptions loadOptions)
        {
            var currentUser = await GetCurrentUserAsync();
            var participants = _context.Users.Where(u => u.Id != currentUser.Id)
                .Select(u => new UserViewModel {
                    EMail = u.Email,
                    UserID = u.Id
                });
            return DataSourceLoader.Load(participants, loadOptions);
        }

        public IActionResult Index()
        {
            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

    }
}
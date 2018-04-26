using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models.HealthCenter
{
    public class Patient : Person
    {

        [ForeignKey("FileID")]
        public virtual File Avatar { get; set; }

        public int? FileID { get; set; }

        public string ApplicationUserID { get; set; }
        
        [ForeignKey("ApplicationUserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Visit> Visits { get; set; }

        public virtual ICollection<PatientBan> Bans { get; set; }

        public bool IsBanned(DateTime date)
        {
            var banned = false;
            foreach (var ban in Bans)
            {
                if (ban.BannedUntil > date )
                {
                    banned = true;
                    break;
                }
            }
            return banned;
        }

        public int NotArrivedCount(DateTime dateUntil)
        {
            var countNotArrived = Visits.Where(v => v.HasArrived == false && v.VisitDateTime <= dateUntil).Count();
            return countNotArrived;
        }

        // All past visits
        public int AllVisitsCount(DateTime dateUntil)
        {
            var allPastVisits = Visits.Where(v => v.VisitDateTime <= dateUntil).Count();
            return allPastVisits;
        }

    }
}

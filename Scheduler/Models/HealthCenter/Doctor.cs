using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models.HealthCenter
{
    public class Doctor : Person
    {
        public Doctor()
        {
            IsDisabled = false;
        }

        [ForeignKey("FileID")]
        public virtual File Avatar { get; set; }
        public int? FileID { get; set; }

        public string ApplicationUserID { get; set; }

        [ForeignKey("ApplicationUserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        public string Position { get; set; }

        public bool IsDisabled { get; set; }

        public virtual ICollection<DoctorScheduleTime> DoctorScheduleTimes { get; set; }
    }
}

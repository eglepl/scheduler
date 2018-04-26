using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models.HealthCenter
{
    public class DoctorScheduleTime
    {
        public DateTime VisitDateTime { get; set; }

        public int DoctorID { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { get; set; }
    }
}

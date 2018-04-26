using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models.HealthCenter
{
    public class DoctorSchedule
    {

        [Column(TypeName = "date")]
        public DateTime VisitDate { get; set; }


        public int DoctorID { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { get; set; }


        public int? VisitScheduleTemplateID { get; set; }

        [ForeignKey("VisitScheduleTemplateID")]
        public virtual VisitScheduleTemplate VisitScheduleTemplate { get; set; }
    }
}

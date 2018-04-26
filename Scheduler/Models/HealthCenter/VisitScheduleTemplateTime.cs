using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models.HealthCenter
{
    public class VisitScheduleTemplateTime
    {
        private DateTime _start;

        public int ID { get; set; }
        public DateTime Start {
            get
            {
                return _start;
            }
            set
            {
                _start = new DateTime(1, 1, 1, value.Hour, value.Minute, 0);
            }
        }
        public int VisitScheduleTemplateID { get; set; }

        [ForeignKey("VisitScheduleTemplateID")]
        public virtual VisitScheduleTemplate VisitScheduleTemplate { get; set; }
    }
}

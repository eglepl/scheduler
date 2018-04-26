using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models.HealthCenter
{
    public class VisitScheduleTemplate
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public virtual ICollection<VisitScheduleTemplateTime> Times { get; set; }

    }
}

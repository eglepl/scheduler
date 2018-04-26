using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.ViewModels.HealthCenter
{
    public class DoctorScheduleUpdateViewModel
    {
        [Required]
        public int VisitScheduleTemplateViewModelID { get; set; }
    }
}

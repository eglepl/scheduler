using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.ViewModels.HealthCenter
{
    public class DoctorScheduleVisitTimeViewModel
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Visit Date Time")]
        public DateTime VisitDateTime { get; set; }
    }
}

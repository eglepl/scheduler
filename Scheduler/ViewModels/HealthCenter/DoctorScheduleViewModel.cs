using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.ViewModels.HealthCenter
{
    public class DoctorScheduleViewModel
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Visit Date")]
        public DateTime VisitDate { get; set; }

        public int? VisitScheduleTemplateID { get; set; }
    }
}

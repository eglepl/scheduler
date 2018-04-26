using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.ViewModels.HealthCenter
{
    public class VisitScheduleTemplateViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Title")]
        public string Title { get; set; }
    }
}

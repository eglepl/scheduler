using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.ViewModels.HealthCenter
{
    public class VisitScheduleTemplateTimeViewModel
    {
        [Required]
        public int ID { get; set; }

        //[Required]
        //public int TemplateID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Time")]
        public DateTime Start { get; set; }

        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Scheduler.Models.HealthCenter.Person;

namespace Scheduler.Models.ManagePatientViewModels
{
    public class PatientViewModel
    { 

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        [Required]
        public SexEnum Sex { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "The {0} must be at least {1} characters long.")]
        [Display(Name = "Personal Code")]
        public string PersonalCode { get; set; }
    }
}

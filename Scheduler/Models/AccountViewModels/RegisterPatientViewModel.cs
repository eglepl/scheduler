using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Scheduler.Models.HealthCenter.Person;

namespace Scheduler.Models.AccountViewModels
{
    public class RegisterPatientViewModel: RegisterViewModel
    {

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BirthDate { get; set; }

        [Required]
        public SexEnum Sex { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "The {0} must be exactly {1} characters long.", MinimumLength = 11)]
        public string PersonalCode { get; set; }
    }
}

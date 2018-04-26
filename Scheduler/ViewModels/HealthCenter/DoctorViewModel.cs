using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Scheduler.Models.HealthCenter.Doctor;
using static Scheduler.Models.HealthCenter.Person;

namespace Scheduler.ViewModels.HealthCenter
{
    public class DoctorViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }


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

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "E-Mail")]
        public string EMail { get; set; }

        [Display(Name = "Notes")]
        public string Notes { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Work Position")]
        public string Position { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 2)]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Free Visits")]
        public int FreeVisitsCount { get; set; }

        public bool IsDisabled { get; set; }
    }
}

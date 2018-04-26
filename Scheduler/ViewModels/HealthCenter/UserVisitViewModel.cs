using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.ViewModels.HealthCenter
{
    public class UserVisitViewModel
    {
        public int ID { get; set; }

        public string PatientNotes { get; set; }

        public string DoctorNotes { get; set; }

        [Required]
        public DateTime VisitDateTime { get; set; }

        [Required]
        public int PatientID { get; set;  }

        [Required]
        public string PatientFullName { get; set; }

        [Required]
        public int DoctorID { get; set; }

        [Required]
        public string DoctorFullName { get; set; }

        [Required]
        public string DoctorPosition { get; set; }

    }
}

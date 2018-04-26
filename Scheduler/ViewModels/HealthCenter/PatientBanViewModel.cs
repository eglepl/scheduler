using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.ViewModels.HealthCenter
{
    public class PatientBanViewModel
    {
        [Required]
        public int ID { get; set; }

        [Required]
        public DateTime BannedUntil { get; set; }

        [Required]
        public int PatientID { get; set; }

        public string PatientFullName { get; set; }   
    }
}

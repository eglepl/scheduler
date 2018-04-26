using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models.ManagePatientViewModels
{
    public class PatientEditViewModel: PatientViewModel
    {
        [Required]
        public int ID { get; set; }
    }
}

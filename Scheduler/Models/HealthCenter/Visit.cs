using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models.HealthCenter
{
    public class Visit
    {
        public Visit()
        {
            IsNotified = false;
            HasArrived = false;
        }
        public int ID { get; set; }

        public string PatientNotes { get; set; }

        public string DoctorNotes { get; set; }
        public bool HasArrived { get; set; }

        public DateTime VisitDateTime { get; set; }

        public bool IsNotified { get; set; }

        public int PatientID { get; set; }

        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }


        public int DoctorID { get; set; }

        [ForeignKey("DoctorID")]
        public virtual Doctor Doctor { get; set; }
    }
}

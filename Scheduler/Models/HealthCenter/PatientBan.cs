using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models.HealthCenter
{
    public class PatientBan
    {
        public int ID { get; set; }

        [Required]
        public DateTime BannedUntil { get; set; }

        public int PatientID { get; set; }


        [ForeignKey("PatientID")]
        public virtual Patient Patient { get; set; }


    }
}

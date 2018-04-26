using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Scheduler.Models.HealthCenter;

namespace Scheduler.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.ViewModels.HealthCenter
{
    public class UserViewModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string EMail { get; set; }
    }
}

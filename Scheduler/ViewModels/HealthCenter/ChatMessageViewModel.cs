using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.ViewModels.HealthCenter
{
    public class ChatMessageViewModel
    {
        [Required]
        public int ID { get; set; }
        
        public string From { get; set; }
        
        public string To { get; set; }
       
        public DateTime MessageTime { get; set; }

        [Required]
        public string ApplicationUserIDFrom { get; set; }

        [Required]
        public string ApplicationUserIDTo { get; set; }

        public string MessageText { get; set; }

        public string Direction { get; set; }


    }
}

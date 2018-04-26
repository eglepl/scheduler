using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models.HealthCenter
{
    public class ChatMessage
    {
        public int ID { get; set; }

        public DateTime MessageTime { get; set; }

        public string Message { get; set; }
        public string ApplicationUserIDFrom { get; set; }
        public string ApplicationUserIDTo { get; set; }

        [ForeignKey("ApplicationUserIDFrom")]
        public virtual ApplicationUser FromUser { get; set; }

        [ForeignKey("ApplicationUserIDTo")]

        public virtual ApplicationUser ToUser { get; set; }
    }
}

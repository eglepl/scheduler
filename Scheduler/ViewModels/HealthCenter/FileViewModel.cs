using Scheduler.Models.HealthCenter;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Scheduler.Models.HealthCenter.File;

namespace Scheduler.ViewModels.HealthCenter
{
    public class FileViewModel
    {
        public int FileId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public FileTypes FileType { get; set; }
        public int UserId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}

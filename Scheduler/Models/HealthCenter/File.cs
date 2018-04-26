using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Scheduler.Models.HealthCenter
{
    public class File : IFormFile
    {
        public enum FileTypes
        {
            Avatar = 1, Photo
        }

        public File()
        {

        }

        public File(IFormFile formFile)
        {
            Load(formFile);
        }

        public void Load(IFormFile formFile) {
            Name = formFile.Name;
            FileName = formFile.FileName;
            ContentType = formFile.ContentType;
            ContentDisposition = formFile.ContentDisposition;
            Length = formFile.Length;
            
            using (var memoryStream = new MemoryStream())
            {
                formFile.CopyTo(memoryStream);
                Content = memoryStream.ToArray();
            }
        }

        public int ID { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }

        public long Length { get; set; }

        [StringLength(100)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        //public virtual Doctor Doctor { get; set; }

        [StringLength(200)]
        public string ContentDisposition { get; set; }

        [NotMapped]
        public IHeaderDictionary Headers {
            get
            {
                return new HeaderDictionary();
            }
            set
            {
                
            }
        }

        [StringLength(100)]
        public string Name { get; set; }

        public Stream OpenReadStream()
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Stream target)
        {
            target.Write(Content, 0, Content.Length);
        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}

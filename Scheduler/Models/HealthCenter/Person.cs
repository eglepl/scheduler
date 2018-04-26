using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models.HealthCenter
{
    public class Person
    {

        public enum SexEnum
        {
            Male = 0,
            Female = 1
        }

        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }
        public SexEnum Sex { get; set; }
        public string PersonalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string EMail { get; set; }
        public string Notes { get; set; }

        public string Address { get; set; }
        public string City { get; set; }


        [NotMapped]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            } 
        }

    }
}

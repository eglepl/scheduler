using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Scheduler.Models.HealthCenter;

namespace Scheduler.Models.ManageViewModels
{
    public class PatientsListViewModel
    {
        public ICollection<Patient> _patients; 

        public ICollection<Patient> Patients {
            get
            {
                if (_patients != null)
                {
                    return _patients;
                }
                else
                {
                    return new List<Patient>();
                }
            }
            set
            {
                _patients = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scheduler.Models.HealthCenter
{
    interface IPatientRepository
    {
        IEnumerable<Patient> GetAllPatients();
        Patient GetPatientById(int patientId);

  
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AppoinmentManagment.BusinessLayer
{
    public class AppoinmentBO
    {
        [Required]
        public string DoctorId { get; set; }

        [Required]
        public DateTime AppointmentDate { get; set; }

        [Required]
        public DateTime AppointmentTime { get; set; }

        
        public string AppointmentStatus { get; set; }

        [Required]
        public string Symptom { get; set; }

        [Required]
        public string Medication { get; set; }

        public string Diesis { get; set; }

        public string Prescription { get; set; }

    }
}

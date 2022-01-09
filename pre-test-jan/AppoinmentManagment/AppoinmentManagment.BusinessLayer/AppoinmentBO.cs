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
        public string AppointmentDate { get; set; }

        [Required]
        public string AppointmentTime { get; set; }

        [Required]
        public string AppointmentStatus { get; set; }

        [Required]
        public string Symptom { get; set; }

        [Required]
        public string Medication { get; set; }

        public string Diesis { get; set; }

        public string Prescription { get; set; }

    }
}

using AppoinmentManagment.BusinessLayer;
using AppoinmentManagment.DataAccessLayer.IRepository;
using AppoinmentManagment.Extensions;
using AppoinmentManagment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppoinmentManagment.Controllers
{
    [Authorize(Roles = "Patient")]
    public class PatientController : Controller
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IAppointmentRepository _appointment;
        private readonly ISpecializationRepository _special;
        private readonly IDoctorRepository _doctor;

        public PatientController(ILogger<PatientController> logger, IAppointmentRepository appointment, ISpecializationRepository special, IDoctorRepository doctor)
        {
            _logger = logger;
            _appointment = appointment;
            _special = special;
            _doctor = doctor;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Appointment()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Appointment(AppoinmentBO abo)
        {
            (int id, _) = HttpContext.GetUserInfo();
            (_, string name) = HttpContext.GetUserInfo();
            _logger.LogInformation("The Appoinment Post method has been called");
            try
            {
                //Query for user existence
                var date = DateTime.Now;
                string appointId = "app"+date.ToString("yyyyMMdd-HHmmssfff");
                bool applicationExists = _appointment.AppointmentAlreadyExists( abo, id);

                if (applicationExists == true)
                {
                    ViewBag.Error = "Appointment for this doctor on the date already exists";
                    return View();
                }

                int result = _appointment.Add(abo,id,name, appointId);
                if (result > 0)
                {
                    _logger.LogInformation("Appointment data Inserted");
                    return RedirectToRoute("Patientdashboard");
                }
                else
                {
                    ViewBag.Error = "Error occured Please contact to It person";
                    return View();
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError($"Exception - '{e}'");
                ViewBag.Error = "Appointment Adding Failed, Please Try again!";
                return View();
            }
        }

        #region API

        [Route("/specialization/all")]
        public JsonResult GetAllSpecialization()
        {
            List<SpecializationModel> sml = _special.GetAllSpecialization();
            return Json(new { data = sml});
        }

        [Route("/getdoctor/{id}")]
        public JsonResult GetDoctorBySpecialization(int id)
        {
            List<DoctorBO> doc = _doctor.GetDoctorBySpecialization(id);

            return Json(new { data = doc});
        }
        #endregion
    }
}

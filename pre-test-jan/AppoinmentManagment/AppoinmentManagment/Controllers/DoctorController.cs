using AppoinmentManagment.BusinessLayer;
using AppoinmentManagment.DataAccessLayer.IRepository;
using AppoinmentManagment.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppoinmentManagment.Controllers
{
    [Authorize(Roles = "Doctor")]
    public class DoctorController : Controller
    {
        private readonly IAppointmentRepository _appointment;

        public DoctorController( IAppointmentRepository appointment)
        {
            _appointment = appointment;
        }

        public IActionResult Index()
        {
            string DrId = HttpContext.GetDrId();
            ListAppoinmentBO labo = new ListAppoinmentBO()
            {
                AppointmentList = _appointment.GetApprovedPaidAppointmentDoctorId(DrId)
            };
            return View(labo);
        }

        [HttpPost]
        public IActionResult Index(ListAppoinmentBO labo)
        {
            string prescription = labo.Appointment.Prescription;
            string appointmentId = labo.Appointment.AppointmentId;
            string desis = labo.Appointment.Diesis;
            (_, string name) = HttpContext.GetUserInfo();
            int result = _appointment.Prescribe(appointmentId, prescription, desis, name);
            if (result > 0)
            {
                ViewBag.Success = "Succesfully prescribed";
                string DrId = HttpContext.GetDrId();
                ListAppoinmentBO abo = new ListAppoinmentBO()
                {
                    AppointmentList = _appointment.GetApprovedPaidAppointmentDoctorId(DrId)
                };
                return View(abo);
            }
            else
            {
                ViewBag.Success = "Error while prescription, please try again";
                return View();
            }
        }

        #region API

        [Route("/appointment/visit/{id}")]
        public JsonResult Visit(string id)
        {
            try
            {
                (_, string name) = HttpContext.GetUserInfo();
                int result = _appointment.VisitAppointment(id, name);
                if (result > 0)
                {
                    return Json(new { success = true, message = "Visit successful" });
                }
                else
                {
                    return Json(new { success = false, message = "Error ." });
                }
            }
            catch (NullReferenceException)
            {
                return Json(new { success = false, message = "Error , please try again!" });
            }
        }

        [Route("/appointment/patient/{id}")]
        public JsonResult Patient(string id)
        {
            AppoinmentBO abo = _appointment.GetAppoinmentById(id);

            return Json( new { data = abo } );
        }
        #endregion
    }
}

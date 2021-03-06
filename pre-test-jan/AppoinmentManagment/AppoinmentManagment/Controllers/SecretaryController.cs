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
    [Authorize(Roles = "Secretary")]
    public class SecretaryController : Controller
    {
        private readonly ILogger<SecretaryController> _logger;
        private readonly IAppointmentRepository _appoinment;


        public SecretaryController(ILogger<SecretaryController> logger, IAppointmentRepository appoinment)
        {
            _logger = logger;
            _appoinment = appoinment;
        }

        public IActionResult Index()
        {
            string DrId = HttpContext.GetDrId();
            ListAppoinmentBO model = new ListAppoinmentBO
            {
                AppointmentList = _appoinment.GetAllApprovedAppoinmentByDrId(DrId)
            };
            return View(model);
        }

        public IActionResult GetAllPendingAppointment()
        {
            string DrId = HttpContext.GetDrId();
            ListAppoinmentBO model = new ListAppoinmentBO
            {
                AppointmentList = _appoinment.GetAllPendingAppoinmentByDrId(DrId)
            };
            return View(model);
        }


        #region API
        [HttpPost]
        [Route("/appointment/Decline/{id}")]
        public IActionResult Decline(string id)
        {
            try
            {
                (_, string name) = HttpContext.GetUserInfo();
                int result = _appoinment.DeclineAppoinment(id,name);
                if (result > 0)
                {
                    return Json(new { success = true, message = "Appoinment Declined successful" });
                }
                else
                {
                    return Json(new { success = false, message = "Error while Declining appoinment." });
                }
            }
            catch (NullReferenceException)
            {
                return Json(new { success = false, message = "Error while Decline, please try again!" });
            }
        }

        [HttpPost]
        [Route("/appointment/Approve/{id}")]
        public IActionResult Approve(string id)
        {
            try
            {
                (_, string name) = HttpContext.GetUserInfo();
                int result = _appoinment.ApproveAppoinment(id,name);
                if (result > 0)
                {
                    return Json(new { success = true, message = "Appoinment approves successful" });
                }
                else
                {
                    return Json(new { success = false, message = "Error while approving appoinment." });
                }
            }
            catch (NullReferenceException)
            {
                return Json(new { success = false, message = "Error while approving, please try again!" });
            }
        }

        [Route("/CountPendingAppointment")]
        public JsonResult CountPendingAppointment()
        {
            string DrId = HttpContext.GetDrId();
            int appointment = _appoinment.CountPendingAppointment(DrId);
            return Json(new { Appointment = appointment });
        }
        #endregion
    }
}

using AppoinmentManagment.BusinessLayer;
using AppoinmentManagment.DataAccessLayer.IRepository;
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
    public class AdminController : Controller
    {
        private readonly IAdminApiRepository _admin;
        private readonly IAuthenticationRepository _auth;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminApiRepository admin, IAuthenticationRepository auth, ILogger<AdminController> logger)
        {
            _admin = admin;
            _auth = auth;
            _logger = logger;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetAllPatient()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetAllDoctor()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult GetAllSpecialization()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddPatient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddPatient(UserModel um)
        {
            _logger.LogInformation("The Register Post method has been called");
            try
            {
                //Query for user existence
                bool userExists = _auth.UserAlreadyExists(um);

                if (userExists == true)
                {
                    ViewBag.Error = "Patient's Name and Email Already Exists";
                    return View();
                }
                //If user doesn't exists it inserts data into database
                int result = _auth.Register(um);
                if (result > 0)
                {
                    _logger.LogInformation("patient's data Inserted");
                    return RedirectToRoute("Admindashboard"); 
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
                ViewBag.Error = "Registration Failed, Please Try again!";
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddDoctor()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddSpecialization()
        {
            return View();
        }



        #region API
        [Authorize(Roles = "Admin")]
        [Route("/CountPatient")]
        public JsonResult CountPatient()
        {
            int patient = _admin.CountPatient();
            return Json(new { Patient = patient });
        }


        [Authorize(Roles = "Admin")]
        [Route("/CountDoctor")]
        public JsonResult CountDoctor()
        {
            int doctor = _admin.CountDoctor();
            return Json(new { Doctor = doctor });
        }

        [Authorize(Roles = "Admin")]
        [Route("/CountSpecialization")]
        public JsonResult CountSpecialization()
        {
            int special = _admin.CountSpecialization();
            return Json(new { Special = special });
        }

        [Authorize(Roles = "Admin")]
        [Route("/Balance")]
        public JsonResult Balance()
        {
            int balance = _admin.TotalBalance();
            return Json(new { Balance = balance });
        }

        [Authorize(Roles = "Admin")]
        [Route("/GetAllPatient")]
        public JsonResult GetAllPatientList()
        {
            List<UserBO> user = _admin.GetAllPatientList();
            return Json(new { data = user});
        }
        #endregion
    }
}

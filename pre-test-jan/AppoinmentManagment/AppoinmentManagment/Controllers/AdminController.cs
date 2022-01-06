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
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminApiRepository _admin;
        private readonly IAuthenticationRepository _auth;
        private readonly ILogger<AdminController> _logger;
        private readonly ISpecializationRepository _special;

        public AdminController(IAdminApiRepository admin, IAuthenticationRepository auth, ILogger<AdminController> logger, ISpecializationRepository special)
        {
            _admin = admin;
            _auth = auth;
            _logger = logger;
            _special = special;
        }

        public IActionResult Index()
        {
            return View();
        }

       
        public IActionResult GetAllPatient()
        {
            return View();
        }

        
        public IActionResult GetAllDoctor()
        {
            return View();
        }

        
        public IActionResult GetAllSpecialization()
        {
            return View();
        }

        
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

        
        public IActionResult AddDoctor()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddSpecialization()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddSpecialization(SpecializationModel sm)
        {
            _logger.LogInformation("The Specialization Post method has been called");
            try
            {
                //Query for user existence
                bool specializationExists = _special.specAlreadyExists(sm);

                if (specializationExists == true)
                {
                    ViewBag.Error = "Specialization Already Exists";
                    return View();
                }
                
                int result = _special.Add(sm);
                if (result > 0)
                {
                    _logger.LogInformation("specialization data Inserted");
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
                ViewBag.Error = "Specialization Adding Failed, Please Try again!";
                return View();
            }
            
        }



        #region API

        [Route("/CountPatient")]
        public JsonResult CountPatient()
        {
            int patient = _admin.CountPatient();
            return Json(new { Patient = patient });
        }

        
        [Route("/CountDoctor")]
        public JsonResult CountDoctor()
        {
            int doctor = _admin.CountDoctor();
            return Json(new { Doctor = doctor });
        }

        
        [Route("/CountSpecialization")]
        public JsonResult CountSpecialization()
        {
            int special = _admin.CountSpecialization();
            return Json(new { Special = special });
        }

        
        [Route("/Balance")]
        public JsonResult Balance()
        {
            int balance = _admin.TotalBalance();
            return Json(new { Balance = balance });
        }

        
        [Route("/GetAllPatient")]
        public JsonResult GetAllPatientList()
        {
            List<UserBO> user = _admin.GetAllPatientList();
            return Json(new { data = user});
        }
        #endregion
    }
}

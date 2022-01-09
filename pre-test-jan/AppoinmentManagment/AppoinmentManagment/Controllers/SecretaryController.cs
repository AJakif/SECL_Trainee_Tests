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
        //private readonly ILogger<SecretaryController> _logger;

        //public SecretaryController(ILogger<SecretaryController> logger)
        //{
        //    _logger = logger;
        //}


        public IActionResult Index()
        {
            return View();
        }


        #region API

        //public JsonResult GetAllAppoinment()
        //{
        //    string DrId = HttpContext.GetDrId();
        //    //AppoinmentBO abo = _appoinment.GetAllAppoinmentByDrId(DrId);
        //    //return Json(new { data = abo});
        //}
        #endregion
    }
}

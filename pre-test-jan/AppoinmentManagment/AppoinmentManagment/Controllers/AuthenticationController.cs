using AppoinmentManagment.BusinessLayer;
using AppoinmentManagment.DataAccessLayer.IRepository;
using AppoinmentManagment.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AppoinmentManagment.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IAuthenticationRepository _auth;
        private readonly IUserRepository _user;
        private readonly ISecretaryRepository _secretary;

        public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationRepository auth, IUserRepository user, ISecretaryRepository secretary)
        {
            _logger = logger;
            _auth = auth;
            _user = user;
            _secretary = secretary;
        }

        [HttpGet]
        [Route("/Registration")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel um)
        {
            _logger.LogInformation("The Register Post methhod has been called");
            try
            {
                //Query for user existence
                bool userExists = _auth.UserAlreadyExists(um);

                if (userExists == true)
                {
                    ViewBag.Error = "Name and Email Already Exists";
                    return View();
                }
                //If user doesn't exists it inserts data into database
                int result = _auth.Register(um);
                if (result > 0)
                {
                    _logger.LogInformation("User data Inserted");
                    return RedirectToRoute("default"); //Redirects to Home accounts index view
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError($"Exception - '{e}'");
                ViewBag.Error = "Registration Failed, Please Try again!";
            }
            return View();
        }

        [HttpGet]
        [Route("/Login")]
        public IActionResult Login()
        {
            _logger.LogInformation("The Login page has been accessed");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginBO lbo)
        {
            try
            {
                UserModel userDetails = _auth.LoginByEmail(lbo);
                int typeId = userDetails.TypeId;
                UserBO user = _user.GetUserType(typeId);
                _logger.LogInformation($"Userdetails '{userDetails}'");

                if (userDetails != null && userDetails.Email != null) //all data should be null checked
                {
                    if (user.Type == "Secretary")
                    {
                        string id = _secretary.GetDrId(userDetails.OId);
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email,userDetails.Email),
                            new Claim("Name", userDetails.Name),
                            new Claim(ClaimTypes.NameIdentifier, userDetails.OId.ToString()),
                            new Claim(ClaimTypes.Role,user.Type),
                            new Claim("DoctorId", id)
                        };
                        _logger.LogInformation("secretary Email, Name and Id , doctor id set on claim");
                        var identity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            // Refreshing the authentication session should be allowed.

                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                            // The time at which the authentication ticket expires. A 
                            // value set here overrides the ExpireTimeSpan option of 
                            // CookieAuthenticationOptions set with AddCookie.
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);
                    }
                    else
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email,userDetails.Email),
                            new Claim("Name", userDetails.Name),
                            new Claim(ClaimTypes.NameIdentifier, userDetails.OId.ToString()),
                            new Claim(ClaimTypes.Role,user.Type)

                        };
                        _logger.LogInformation("User Email, Name and Id set on claim");
                        var identity = new ClaimsIdentity(
                            claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            AllowRefresh = true,
                            // Refreshing the authentication session should be allowed.

                            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                            // The time at which the authentication ticket expires. A 
                            // value set here overrides the ExpireTimeSpan option of 
                            // CookieAuthenticationOptions set with AddCookie.
                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);
                    }
                   
                    
                    if(user.Type == "Patient")
                    {
                        return RedirectToRoute("Patientdashboard");
                    }
                    else if(user.Type == "Admin")
                    {
                        return RedirectToRoute("Admindashboard");
                    }
                    else if (user.Type == "Doctor")
                    {
                        return RedirectToRoute("Doctordashboard");
                    }
                    else if(user.Type == "Secretary")
                    {
                        return RedirectToRoute("Secretarydashboard");
                    }
                }
                else
                {
                    ViewBag.Error = "Wrong Email & Password, please try again";
                    return View();
                }
            }
            catch (NullReferenceException e)
            {
                _logger.LogError("Exception", e);
                ViewBag.Error = "There is an error while login, please contact admin";
            }
            return View();
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            _logger.LogInformation("User logout called");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("User logged out");
            return RedirectToRoute("default");
        }
    }
}

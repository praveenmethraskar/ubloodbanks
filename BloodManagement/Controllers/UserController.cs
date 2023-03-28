using BloodManagement.Filters;
using BloodManagement.Models;
using BloodManagement.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace BloodManagement.Controllers
{
    
    public class UserController : Controller
    {
        IUserRL userRL;
       
        public UserController(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration([Bind] UserRegistrationModel user)
        {
           
            if (ModelState.IsValid)
            {
                userRL.Registration(user);
                return RedirectToAction("Login");
            }
            return View(user);
        }


        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin,Employee")]
        public IActionResult Login([Bind] UserLoginModel loginModel)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {

                var result = userRL.UserLogin(loginModel);

                if (result != null)
                {
                    HttpContext.Session.SetInt32("users_id", result.users_id);
                    HttpContext.Session.SetString("name", result.fullname);
                    HttpContext.Session.SetString("Email", result.mail);
                    HttpContext.Session.SetString("Password", result.Password);

                    return RedirectToAction("Dashboard");

                    //message = "Username and/or password is correct.";
                    //Console.WriteLine(message);

                }
                else
                {
                    return RedirectToAction("Login");
                }

            }
            return null;
        }

        //[Authenticated]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
    
            return RedirectToAction("Login");
        }


        [HttpGet]

        public IActionResult Dashboard()
        {
            return View();
        }



        [HttpGet]

        public IActionResult Addprofile()
        {
            int users_id = (int)HttpContext.Session.GetInt32("users_id");
            var userslist = userRL.Getusers(users_id);
            ViewBag.Model = userslist;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Addprofile(AddMoreDetails more)
        {
            if (ModelState.IsValid)
            {
                int users_id = (int)HttpContext.Session.GetInt32("users_id");
                var result = userRL.Profile(more, users_id);
                if (result != null)
                {
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    return RedirectToAction("login");
                }
            }
            return View(more);
        }
        [HttpGet]
        //public IActionResult Addprofiles()
        //{
        //    int users_id = (int)HttpContext.Session.GetInt32("users_id");
        //    var userslist = userRL.Getusers(users_id);
        //    ViewBag.Model = userslist;
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Addprofiles([Bind] AddmoreModel more)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        int users_id = (int)HttpContext.Session.GetInt32("users_id");
        //        var result = userRL.Profiles(more, users_id);
        //        if (result != null)
        //        {
        //            return RedirectToAction("Dashboard");
        //        }
        //        else
        //        {
        //            return RedirectToAction("login");
        //        }
        //    }
        //    return View(more);
        //}
        [HttpGet]
        public IActionResult viewprofile()
        {
            int users_id = (int)HttpContext.Session.GetInt32("users_id");
            var usersprofile = userRL.Getprofile(users_id);
            ViewBag.Model = usersprofile;
            return View(usersprofile);
        }
        [HttpGet]
        public IActionResult Donations()
        {
            var usersprofile = userRL.GetHospitals();
            ViewBag.Model = usersprofile;
            return View();

        }

        [HttpPost]
        public IActionResult Donations([Bind] Donations donations)
        {

            if (ModelState.IsValid)
            {
                int users_id = (int)HttpContext.Session.GetInt32("users_id");
                var result = userRL.EnterDonations(donations, users_id);
                
                if (result != null)
                {
                    HttpContext.Session.SetString("hospitals", result.hospital);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    return RedirectToAction("Dashboard");
                }
            }
            return View(donations);
            //return View();
        }
        [HttpGet]
        public IActionResult Listdonations()
        {
            int users_id = (int)HttpContext.Session.GetInt32("users_id");
            //string hospital = HttpContext.Session.GetString("hospitals");

            var usersprofile = userRL.Listdonations(users_id);
            //ViewBag.Model = usersprofile;
            return View(usersprofile);
        }
    }
}

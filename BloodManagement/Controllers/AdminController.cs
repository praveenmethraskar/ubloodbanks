using BloodManagement.Models;
using BloodManagement.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodManagement.Controllers
{
    public class AdminController : Controller
    {

        IAdminRL adminRL;

        public AdminController(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdminRegistration()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AdminRegistration([Bind] AdminRegisterModel admin)
        {

            if (ModelState.IsValid)
            {
                adminRL.Register(admin);
                return RedirectToAction("Login");
            }
            return View(admin);
        }


        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin,Employee")]
        public IActionResult Login([Bind] AdminLoginModel loginModel)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {

                var result = adminRL.adminLogin(loginModel);

                if (result != null)
                {
                    HttpContext.Session.SetInt32("admin_id", result.admin_id);
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


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login");
        }
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult viewdetails()
        {
            var usersprofile = adminRL.Getdetails();
            ViewBag.Model = usersprofile;
            return View();
        }

        [HttpGet]
        public IActionResult AddHospitals()
        {
            var usersprofile = adminRL.GetHospitals();
            ViewBag.Model = usersprofile;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddHospitals([Bind] HospitalModel hospital)
        {

            if (ModelState.IsValid)
            {
                adminRL.Addhospitals(hospital);
                return RedirectToAction("AddHospitals");
            }
            return View(hospital);
        }
        [HttpGet]
        public IActionResult viewHospitals()
        {
            var usersprofile = adminRL.GetHospitals();
            ViewBag.Model = usersprofile;
            return View();
            RedirectToAction("AddHospitals");
        }
    }
}

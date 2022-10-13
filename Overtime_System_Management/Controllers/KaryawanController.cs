using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Overtime_System_Management.Controllers
{
    public class KaryawanController : Controller
    {
        public IActionResult Index()
        {
            var role = HttpContext.Session.GetString("Role");
            var fullName = HttpContext.Session.GetString("FullName");
            var id = HttpContext.Session.GetString("Id");
            var gender = HttpContext.Session.GetString("Gender");
            ViewBag.Id = id;
            ViewBag.FullName = fullName;
            ViewBag.Role = role;
            ViewBag.Gender = gender;
            if (role == null)
            {
                
                TempData["Unauthorized"] = "true";
                return RedirectToAction("LoginPage", "Account");
            }
            if (role != "User")
            {
                return View("~/Views/Shared/Forbidden.cshtml");
            }
            return View();
        }


        public IActionResult Dashboard()
        {
            var role = HttpContext.Session.GetString("Role");
            var fullName = HttpContext.Session.GetString("FullName");
            var id = HttpContext.Session.GetString("Id");
            var gender = HttpContext.Session.GetString("Gender");
            ViewBag.Id = id;
            ViewBag.FullName = fullName;
            ViewBag.Role = role;
            ViewBag.Gender = gender;
            if (role == null)
            {

                TempData["Unauthorized"] = "true";
                return RedirectToAction("LoginPage", "Account");
            }
            if (role != "User")
            {
                return View("~/Views/Shared/Forbidden.cshtml");
            }
            return View();
        }
    }
}

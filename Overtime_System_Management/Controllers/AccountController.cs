using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Overtime_System_Management.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Overtime_System_Management.Controllers
{
    public class AccountController : Controller
    {
        HttpClient HttpClient;

        public IActionResult LoginPage()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != null)
            {
                if (role == "User")
                {
                    return RedirectToAction("Index", "Karyawan");
                }
                else if (role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {

            //string address = "https://localhost:44372/api/Account/login";
            string address = "https://localhost:17828/api/Account/login";
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(address)
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");
            var result = HttpClient.PostAsync(address, content).Result;
            if (result.IsSuccessStatusCode)
            {
                var data = JsonConvert.DeserializeObject<ResponseClient>(await result.Content.ReadAsStringAsync());
                HttpContext.Session.SetString("Role", data.data.Role);
                HttpContext.Session.SetString("FullName", data.data.FullName);
                HttpContext.Session.SetString("Id", data.data.Id.ToString());
                HttpContext.Session.SetString("Email", data.data.Email);
                if (data.data.Role == "User")
                {
                    return RedirectToAction("Index", "Karyawan");
                }
                else if (data.data.Role == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }

            }
            TempData["BadRequest"] = "true";
            return RedirectToAction("LoginPage");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("LoginPage");
        }

        public IActionResult ChangePassword()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role != null)
            {
                //if (role != "User")
                //{
                //    return View("~/Views/Shared/Forbidden.cshtml");
                //}
                var fullName = HttpContext.Session.GetString("FullName");
                var email = HttpContext.Session.GetString("Email");
                ViewBag.FullName = fullName;
                ViewBag.Email = email;
                ViewBag.Role = role;
                return View();
            }
            TempData["Unauthorized"] = "true";
            return RedirectToAction("LoginPage");
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {

            //string address = "https://localhost:44372/api/Account/ChangePassword";
            string address = "https://localhost:17828/api/Account/ChangePassword";
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri(address)
            };

            StringContent content = new StringContent(JsonConvert.SerializeObject(changePassword), Encoding.UTF8, "application/json");
            var result = HttpClient.PostAsync(address, content).Result;
            var fullName = HttpContext.Session.GetString("FullName");
            var email = HttpContext.Session.GetString("Email");
            ViewBag.FullName = fullName;
            ViewBag.Email = email;
            if (result.IsSuccessStatusCode)
            {
                ViewBag.Success = "true";
                return View();
            }
            ViewBag.Error = "true";
            return View();
        }
    }
}

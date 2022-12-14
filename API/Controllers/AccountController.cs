using API.Repositories.Data;
using API.ViewModel;
using API.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AccountController : ControllerBase
    {
        /*
         * Login
         * Register
         * Change Password
         * Forget Password
         */

        AccountRepository accountRepository;

        public AccountController(AccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        // requirement login -> email & password
        // response login -> Id Karyawan, FullName, Email, Role (JWT->JSON Web Token)
        [HttpPost]
        [Route("Login")]
        public IActionResult Login (Login login)
        {
            var data = accountRepository.Login(login);
            if (data != null)
                return Ok(new { message = "Login Succeeded", statusCode = 200, data = data });
            return BadRequest(new { message = "Login Failed", statusCode = 400 });
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegistrasiKaryawan register)
        {
            var result = accountRepository.RegistrasiKaryawan(register);
            if (result > 0)
                return Ok(new { statusCode = 200, message = "Registration Succeeded", data = result });
            return BadRequest(new { statusCode = 400, message = "Registration Failed" });
        }

        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            var result = accountRepository.ChangePassword(changePassword);
            if (result > 0)
                return Ok(new { statusCode = 200, message = "Change Password Succeeded", data = result });
            return BadRequest(new { statusCode = 400, message = "Change Password Failed" });
        }
    }
}

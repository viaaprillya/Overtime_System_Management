using API.Repositories.Data;
using API.ViewModel;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
                return Ok(new { message = "berhasil login", statusCode = 200, data = data });
            return BadRequest(new { message = "gagal login", statusCode = 400, data = data });
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegistrasiKaryawan register)
        {
            var result = accountRepository.RegistrasiKaryawan(register);
            if (result > 0)
                return Ok(new { statusCode = 200, message = "Registration Succeded" });
            return BadRequest(new { statusCode = 400, message = "Registration Failed" });
        }

        [HttpPost]
        [Route("ChangePassword")]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            var result = accountRepository.ChangePassword(changePassword);
            if (result > 0)
                return Ok(new { statusCode = 200, message = "Change Password Succeded" });
            return BadRequest(new { statusCode = 400, message = "Change Password Failed" });
        }
    }
}

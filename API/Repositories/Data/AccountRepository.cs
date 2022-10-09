using API.Context;
using API.Models;
using API.ViewModel;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BCrypt;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Data
{
    public class AccountRepository
    {
        MyContext myContext;

        public AccountRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public ResponseLogin Login(Login login)
        {
            var data = myContext.UserRole
                .Include(x => x.Role)
                .Include(x => x.User)
                .Include(x => x.User.Karyawan)
                .FirstOrDefault(x =>
                    x.User.Karyawan.Email.Equals(login.Email));

            if (data != null)
            {
                bool verifiedPassword = Hashing.ValidatePassword(login.Password, data.User.Password);
                if (verifiedPassword == true)
                {
                    ResponseLogin responseLogin = new ResponseLogin()
                    {
                        Id = data.User.ID,
                        FullName = data.User.Karyawan.NamaLengkap,
                        Email = data.User.Karyawan.Email,
                        Role = data.Role.Name
                    };
                    return responseLogin;
                }
            }
            return null;
        }

        [HttpPost]
        public int RegistrasiKaryawan (RegistrasiKaryawan register)
        {
            var password = register.NomerTelepon; //password default menggunakan nomer telepon
            string passwordHash = Hashing.HashPassword(password);
            Karyawan karyawan = new Karyawan();
            karyawan.NamaLengkap = register.NamaLengkap;
            karyawan.Email = register.Email;
            karyawan.NomerRekening = register.NomerRekening;
            karyawan.NomerTelepon = register.NomerTelepon;
            karyawan.JabatanID = register.JabatanID;
            myContext.Karyawan.Add(karyawan);
            myContext.SaveChanges();

            //Tambah User
            User user = new User();
            user.Karyawan = karyawan;
            user.Password = passwordHash;
            myContext.Add(user);
            myContext.SaveChanges();

            //Tambah Role Default RoleId=2 yaitu sebagai user
            UserRole userRole = new UserRole();
            userRole.UserId = user.ID;
            userRole.RoleId = 2;
            myContext.Add(userRole);
            var result = myContext.SaveChanges();
            return result;
        }

        public int ChangePassword(ChangePassword changePassword)
        {
            var data = myContext.User
                .FirstOrDefault(x =>
                    x.Karyawan.Email.Equals(changePassword.Email));
            if (data != null && Hashing.ValidatePassword(changePassword.OldPassword, data.Password))
            {
                string newPasswordHash = Hashing.HashPassword(changePassword.NewPassword);
                data.Password = newPasswordHash; myContext.User.Update(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
            
        }

    }

    public class Hashing
    {
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }

        public static bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}

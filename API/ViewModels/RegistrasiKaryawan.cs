using API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace API.ViewModels
{
    public class RegistrasiKaryawan
    {
        public string NamaLengkap { get; set; }
        public string Email { get; set; }
        public string NomerRekening { get; set; }
        public string NomerTelepon { get; set; }
        public int JabatanID { get; set; }
        public DateTime Tanggal_Lahir { get; set; }
        // 1 = Perempuan, 0 = Laki-Laki
        public Boolean Gender {get; set;}
        public DateTime Tanggal_Masuk { get; set; }
    }
}

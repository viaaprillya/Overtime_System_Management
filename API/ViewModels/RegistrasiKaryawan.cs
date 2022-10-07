using API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.ViewModels
{
    public class RegistrasiKaryawan
    {
            public string NamaLengkap { get; set; }
            public string Email { get; set; }
            public string NomerRekening { get; set; }
            public string NomerTelepon { get; set; }
            public int JabatanID { get; set; }
    }
}

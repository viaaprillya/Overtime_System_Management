using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Gaji
    {
        [Key]
        public int ID { get; set; }
        public Karyawan karyawan = new Karyawan();
        [ForeignKey("karyawan")]
        public int KaryawanID { get; set; }
        public int Bulan { get; set; }
        public int Tahun { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class User
    {
        public Karyawan Karyawan { get; set; }
        [Key]
        [ForeignKey("Karyawan")]
        public int ID { get; set; }
        public string Password { get; set; }
    }
}

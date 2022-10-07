using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Gaji
    {
        [Key]
        public int ID { get; set; }
        public int Bulan { get; set; }
        public int Tahun { get; set; }
        public Karyawan Karyawan { get; set; }
        [ForeignKey("Karyawan")]
        public int KaryawanID {get; set;}
    }
}

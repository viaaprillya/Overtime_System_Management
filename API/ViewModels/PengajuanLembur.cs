using API.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace API.ViewModels
{
    public class PengajuanLembur
    {
        public int KaryawanID { get; set; }
        public int Durasi { get; set; }
        public DateTime Tanggal { get; set; }
        public string Keterangan { get; set; }
    }
}

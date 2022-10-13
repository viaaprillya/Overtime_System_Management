using System;
namespace API.ViewModels
{
    public class EditLembur
    {
        public int ID { get; set; }
        public int KaryawanID { get; set; }
        public int Durasi { get; set; }
        public DateTime Tanggal { get; set; }
        public string Keterangan { get; set; }
        public string Approval { get; set; }
    }
}

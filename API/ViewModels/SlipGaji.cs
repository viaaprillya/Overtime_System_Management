using System;

namespace API.ViewModels
{
    public class SlipGaji
    {
        public int KaryawanID;
        public string NamaKaryawan { get; set; }
        public int Bulan { get; set; }
        public int Tahun { get; set; }
        public int GajiPokok { get; set; }
        public int Tunjangan { get; set; }
        public double TotalBonusLembur { get; set; }
        public double TotalGaji { get; set; }

    }
}

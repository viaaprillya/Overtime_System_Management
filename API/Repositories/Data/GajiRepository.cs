using API.Context;
using API.Models;
using API.ViewModels;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;

namespace API.Repositories.Data
{
    public class GajiRepository
    {
        MyContext myContext;
        public GajiRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public SlipGaji CetakSlipGaji (CetakSlipGaji cetakSlipGaji)
        {
            SlipGaji slipGaji = new SlipGaji();
            var data = myContext.Gaji.FirstOrDefault(x =>
                    x.KaryawanID.Equals(cetakSlipGaji.KaryawanID)&& x.Bulan==cetakSlipGaji.Bulan && x.Tahun == cetakSlipGaji.Tahun);
            var karyawan = myContext.Karyawan.Find(cetakSlipGaji.KaryawanID);
            var dataLembur = myContext.Lembur
                    .Include(x => x.Karyawan)
                    .Where(x =>
                    x.Karyawan.ID.Equals(cetakSlipGaji.KaryawanID) &&
                    x.Tanggal.Month == cetakSlipGaji.Bulan &&
                    x.Tanggal.Year == cetakSlipGaji.Tahun
                    )
                    .GroupBy(x => x.KaryawanID)
                    .Select(x => new { totalLembur = x.Sum(x => x.Durasi) }).FirstOrDefault();
 
            var lembur = 0;

            if (dataLembur != null)
            {
                lembur = dataLembur.totalLembur;
            }

            var gaji = myContext.Jabatan.Find(karyawan.JabatanID).GajiPokok;
            var tunjangan = myContext.Jabatan.Find(karyawan.JabatanID).Tunjangan;
            /* Upah Lembur tiap Jam = 0,5% dari Gaji */
            double totalLembur = (double)lembur* (0.005 * (double)gaji);

            if (data==null) {
                Post(cetakSlipGaji);
            }
            var slip = Get(cetakSlipGaji);
            slipGaji.KaryawanID = slip.KaryawanID;
            slipGaji.NamaKaryawan = karyawan.NamaLengkap;
            slipGaji.Tahun = slip.Tahun;
            slipGaji.Bulan = slip.Bulan;
            slipGaji.GajiPokok = gaji;
            slipGaji.Tunjangan = tunjangan;
            double totalGaji = gaji + tunjangan + totalLembur;
            slipGaji.TotalGaji = totalGaji;
            return slipGaji;
        }

        public Gaji Get(CetakSlipGaji cetakSlipGaji)
        {
            var data = myContext.Gaji.FirstOrDefault(x =>
                    x.KaryawanID.Equals(cetakSlipGaji.KaryawanID) && x.Bulan == cetakSlipGaji.Bulan && x.Tahun == cetakSlipGaji.Tahun);
            return data;

        }


        public int Post(CetakSlipGaji cetakSlipGaji)
        {
            Gaji gaji = new Gaji();
            gaji.KaryawanID = cetakSlipGaji.KaryawanID;
            gaji.Bulan = cetakSlipGaji.Bulan;
            gaji.Tahun = cetakSlipGaji .Tahun;
            myContext.Gaji.Add(gaji);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}

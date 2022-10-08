using API.Context;
using API.Models;
using API.ViewModels;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;

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

            slipGaji.KaryawanID = cetakSlipGaji.KaryawanID;
            slipGaji.NamaKaryawan = karyawan.NamaLengkap;
            slipGaji.Tahun = cetakSlipGaji.Tahun;
            slipGaji.Bulan = cetakSlipGaji.Bulan;
            slipGaji.TotalBonusLembur = totalLembur;
            slipGaji.GajiPokok = gaji;
            slipGaji.Tunjangan = tunjangan;
            double totalGaji = gaji + tunjangan + totalLembur;
            slipGaji.TotalGaji = totalGaji;
            return slipGaji;
        }

        public List<SlipGaji> Get()
        {
            var data = myContext.Lembur
                    .Include(x => x.Karyawan)
                    .Include(x => x.Karyawan.Jabatan)
                    .GroupBy(x => new { x.KaryawanID, x.Tanggal.Month, x.Tanggal.Year})
                    .Select(x => 
                    new { 
                        karyawanID = x.Key.KaryawanID,
                        bulan = x.Key.Month,
                        tahun = x.Key.Year,
                    }).ToList();

            List<SlipGaji> listSlip = new List<SlipGaji>();
            foreach (var d in data)
            {
                CetakSlipGaji cetakSlipGaji = new CetakSlipGaji();
                cetakSlipGaji.KaryawanID = d.karyawanID;
                cetakSlipGaji.Tahun = d.tahun;
                cetakSlipGaji.Bulan = d.bulan;

                SlipGaji slip = new SlipGaji();
                slip = CetakSlipGaji(cetakSlipGaji);
                listSlip.Add(slip);
            }


            return listSlip;
        }

    }
}

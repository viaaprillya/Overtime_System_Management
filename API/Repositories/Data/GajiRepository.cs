using API.Context;
using API.Models;
using API.ViewModels;
using API.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace API.Repositories.Data
{
    public class GajiRepository
    {
        MyContext myContext;
        public GajiRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Post(CetakSlipGaji cetakSlipGaji)
        {
            var result = 0;
            var dataGaji = myContext.Gaji.ToList();
            if (dataGaji.Count == 0)
            {
                Gaji gaji = new Gaji();
                gaji.KaryawanID = cetakSlipGaji.KaryawanID;
                gaji.Bulan = cetakSlipGaji.Bulan;
                gaji.Tahun = cetakSlipGaji.Tahun;
                myContext.Gaji.Add(gaji);
                result = myContext.SaveChanges();
            }
            else
            {
                var isDataExist = myContext.Gaji
                    .Where(x => x.KaryawanID == cetakSlipGaji.KaryawanID &&
                    x.Tahun == cetakSlipGaji.Tahun && x.Bulan == cetakSlipGaji.Bulan).FirstOrDefault();
                if (isDataExist == null)
                {
                    Gaji gaji = new Gaji();
                    gaji.KaryawanID = cetakSlipGaji.KaryawanID;
                    gaji.Bulan = cetakSlipGaji.Bulan;
                    gaji.Tahun = cetakSlipGaji.Tahun;
                    myContext.Gaji.Add(gaji);
                    result = myContext.SaveChanges();
                }
            }
            return result;
        }

        public int Put(Gaji gaji)
        {
            var data = myContext.Gaji.Find(gaji.ID);
            data.KaryawanID = gaji.KaryawanID;
            data.Bulan = gaji.Bulan;
            data.Tahun = gaji.Tahun;
            myContext.Gaji.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public SlipGaji CetakSlipGaji(CetakSlipGaji cetakSlipGaji)
        {
            isiTabelGaji();
            var gaji = myContext.Gaji
                .Where(x => x.KaryawanID==cetakSlipGaji.KaryawanID && x.Bulan==cetakSlipGaji.Bulan && x.Tahun==cetakSlipGaji.Tahun).FirstOrDefault();
            var cekLembur = myContext.Lembur
                    .Where(x =>
                    x.Karyawan.ID.Equals(cetakSlipGaji.KaryawanID) &&
                    x.Tanggal.Month == cetakSlipGaji.Bulan &&
                    x.Tanggal.Year == cetakSlipGaji.Tahun
                    && x.Approval == "Approved"
                    ).FirstOrDefault();

            var dataKaryawan = myContext.Karyawan.Find(cetakSlipGaji.KaryawanID);
            var gajiPokok = myContext.Jabatan.Find(dataKaryawan.JabatanID).GajiPokok;
            var tunjangan = myContext.Jabatan.Find(dataKaryawan.JabatanID).Tunjangan;
            if (gaji != null)
            {
                if (cekLembur != null)
                {
                    var dataLembur = myContext.Lembur
                        .Include(x => x.Karyawan)
                        .Where(x =>
                        x.Karyawan.ID.Equals(cetakSlipGaji.KaryawanID) &&
                        x.Tanggal.Month == cetakSlipGaji.Bulan &&
                        x.Tanggal.Year == cetakSlipGaji.Tahun
                        && x.Approval == "Approved"
                        )
                        .GroupBy(x => x.KaryawanID)
                        .Select(x => new { totalLembur = x.Sum(x => x.Durasi) }).FirstOrDefault();

                    var lembur = dataLembur.totalLembur;
                    double persenUpah = (double)1 /173;


                    /* Upah Lembur tiap Jam = 1/173 dari Gaji rate 1,5 kali untuk 1 jam pertama & rate 2 kali untuk jam berikutnya */
                    double totalLembur = (1.5 * persenUpah * (double)gajiPokok) + ((lembur - 1) * (2 * persenUpah * (double)gajiPokok));
                    //double totalLembur = (double)lembur * (persenUpah * (double)gajiPokok);

                    SlipGaji slipGaji = new SlipGaji();
                    slipGaji.KaryawanID = cetakSlipGaji.KaryawanID;
                    slipGaji.NamaKaryawan = dataKaryawan.NamaLengkap;
                    slipGaji.Tahun = cetakSlipGaji.Tahun;
                    slipGaji.Bulan = cetakSlipGaji.Bulan;
                    slipGaji.TotalBonusLembur = totalLembur;
                    slipGaji.GajiPokok = gajiPokok;
                    slipGaji.Tunjangan = tunjangan;
                    double totalGaji = gajiPokok + tunjangan + totalLembur;
                    slipGaji.TotalGaji = totalGaji;
                    return slipGaji;
                }
                else
                {
                    SlipGaji slipGajiNoLembur = new SlipGaji();
                    slipGajiNoLembur.KaryawanID = cetakSlipGaji.KaryawanID;
                    slipGajiNoLembur.NamaKaryawan = dataKaryawan.NamaLengkap;
                    slipGajiNoLembur.Tahun = cetakSlipGaji.Tahun;
                    slipGajiNoLembur.Bulan = cetakSlipGaji.Bulan;
                    slipGajiNoLembur.GajiPokok = gajiPokok;
                    slipGajiNoLembur.Tunjangan = tunjangan;
                    double totalGajiTanpaLembur = gajiPokok + tunjangan;
                    slipGajiNoLembur.TotalGaji = totalGajiTanpaLembur;
                    return slipGajiNoLembur;
                }
            }
            return null;

        }

        public void isiTabelGaji()
        {
            /*
            var tanggal = myContext.Lembur
                .GroupBy(x => new { x.Tanggal.Month, x.Tanggal.Year })
                 .Select(x =>
                    new
                    {
                        bulan = x.Key.Month,
                        tahun = x.Key.Year,
                    }).ToList();
            var karyawan = myContext.Karyawan.ToList();


            foreach (var k in karyawan)
            {
                foreach (var t in tanggal)
                {
                    CetakSlipGaji cetakBaru = new CetakSlipGaji();
                    cetakBaru.KaryawanID = k.ID;
                    cetakBaru.Bulan = t.bulan;
                    cetakBaru.Tahun = t.tahun;
                    Post(cetakBaru);
                }
            }
            */

            var karyawan = myContext.Karyawan.ToList();
            var tanggal_sekarang = DateTime.Now.Date;

            foreach (var k in karyawan)
            {
                var tanggal_masuk = myContext.Karyawan.Find(k.ID).Tanggal_Masuk;
                for (DateTime date = tanggal_masuk; date.Date <= tanggal_sekarang; date = date.AddMonths(1))
                {
                    CetakSlipGaji cetakBaru = new CetakSlipGaji();
                    cetakBaru.KaryawanID = k.ID;
                    cetakBaru.Bulan = date.Month;
                    cetakBaru.Tahun = date.Year;
                    Post(cetakBaru);
                }
            }


        }

        public List<SlipGaji> Get()
        {
            isiTabelGaji();
            var karyawan = myContext.Karyawan.ToList();
            var gaji = myContext.Gaji.ToList();

            List<SlipGaji> listSlip = new List<SlipGaji>();
            foreach (var g in gaji)
            {
                foreach(var k in karyawan)
                {
                    if (g.KaryawanID == k.ID )
                    {
                        CetakSlipGaji cetak = new CetakSlipGaji();
                        cetak.KaryawanID = g.KaryawanID;
                        cetak.Bulan = g.Bulan;
                        cetak.Tahun = g.Tahun;

                        SlipGaji slip = new SlipGaji();
                        slip = CetakSlipGaji(cetak);
                        listSlip.Add(slip);
                    }
                }
            }
            return listSlip;
        }

        public List<SlipGaji> Get(int idKaryawan)
        {
            isiTabelGaji();
            var gaji = myContext.Gaji.ToList();

            List<SlipGaji> listSlip = new List<SlipGaji>();
            foreach (var g in gaji)
            {
                if (g.KaryawanID == idKaryawan)
                {
                    CetakSlipGaji cetak = new CetakSlipGaji();
                    cetak.KaryawanID = g.KaryawanID;
                    cetak.Bulan = g.Bulan;
                    cetak.Tahun = g.Tahun;

                    SlipGaji slip = new SlipGaji();
                    slip = CetakSlipGaji(cetak);
                    listSlip.Add(slip);
                }
            }
            return listSlip;
        }
    }
}

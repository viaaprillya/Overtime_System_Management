using API.Context;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories.Data
{
    public class LemburRepository : ILemburRepository
    {
        MyContext myContext;
        public LemburRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(int id)
        {
            var data = myContext.Lembur.Find(id);
            myContext.Lembur.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Lembur> Get()
        {
            var data = myContext.Lembur.Include(x => x.Karyawan).ToList();
            return data;
        }

        public Lembur Get(int id)
        {
            var data = myContext.Lembur.Include(x => x.Karyawan).Where(x => x.ID == id).FirstOrDefault();
            return data;
        }

        public List<Lembur> GetByKaryawanId(int karyawanId)
        {
            var data = myContext.Lembur.Where(x => x.KaryawanID == karyawanId).ToList();
            return data;
        }
        public int Post(PengajuanLembur pengajuanLembur)
        {
            string approval = "Processing";
            Lembur lembur = new Lembur();
            lembur.KaryawanID = pengajuanLembur.KaryawanID;
            lembur.Durasi = pengajuanLembur.Durasi;
            lembur.Tanggal = pengajuanLembur.Tanggal;
            lembur.Keterangan = pengajuanLembur.Keterangan;
            lembur.Approval = approval;
            myContext.Lembur.Add(lembur);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Put(EditLembur lembur)
        {
            var data = myContext.Lembur.Find(lembur.ID);
            data.ID = lembur.ID;
            data.KaryawanID = lembur.KaryawanID;
            data.Durasi = lembur.Durasi;
            data.Tanggal = lembur.Tanggal;
            data.Keterangan = lembur.Keterangan;
            data.Approval = lembur.Approval;
            myContext.Lembur.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}

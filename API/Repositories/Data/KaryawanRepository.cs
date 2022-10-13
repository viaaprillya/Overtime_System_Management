using API.Context;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories.Data
{
    public class KaryawanRepository : IKaryawanRepository
    {
        MyContext myContext;
        public KaryawanRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(int id)
        {
            //var dataGaji = myContext.Gaji.Find(id);
            var dataKaryawan = myContext.Karyawan.Find(id);
            //myContext.Gaji.Remove(dataGaji);
            myContext.SaveChanges();
            myContext.Karyawan.Remove(dataKaryawan);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Karyawan> Get()
        {
            var data = myContext.Karyawan.Include(x => x.Jabatan).ToList();
            return data;
        }

        public Karyawan Get(int id)
        {
            var data = myContext.Karyawan.Include(x => x.Jabatan).Where(x => x.ID == id).FirstOrDefault();
            return data;
        }

        public int Put(EditKaryawan karyawan)
        {
            var data = myContext.Karyawan.Find(karyawan.ID);
            data.NamaLengkap = karyawan.NamaLengkap;
            data.Email = karyawan.Email;
            data.NomerRekening = karyawan.NomerRekening;
            data.NomerTelepon = karyawan.NomerTelepon;
            data.JabatanID = karyawan.JabatanID;
            myContext.Karyawan.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}

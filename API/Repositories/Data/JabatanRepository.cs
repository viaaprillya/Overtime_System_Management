using API.Context;
using API.Models;
using API.Repositories.Interface;
using API.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace API.Repositories.Data
{
    public class JabatanRepository : IJabatanRepository

    {
        MyContext myContext;
        public JabatanRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Delete(int id)
        {
            var data = myContext.Jabatan.Find(id);
            myContext.Jabatan.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Jabatan> Get()
        {
            var data = myContext.Jabatan.ToList();
            return data;
        }

        public Jabatan Get(int id)
        {
            var data = myContext.Jabatan.Find(id);
            return data;
        }

        public int Post(InputJabatan inputJabatan)
        {
            Jabatan jabatan = new Jabatan();
            jabatan.NamaJabatan = inputJabatan.NamaJabatan;
            jabatan.GajiPokok = inputJabatan.GajiPokok;
            jabatan.Tunjangan = inputJabatan.Tunjangan;
            myContext.Jabatan.Add(jabatan);
            var result = myContext.SaveChanges();
            return result;
        }

        public int Put(Jabatan jabatan)
        {
            var data = myContext.Jabatan.Find(jabatan.ID);
            data.NamaJabatan = jabatan.NamaJabatan;
            data.GajiPokok = jabatan.GajiPokok;
            data.Tunjangan = jabatan.Tunjangan;
            myContext.Jabatan.Update(data);
            var result = myContext.SaveChanges();
            return result;
        }
    }
}

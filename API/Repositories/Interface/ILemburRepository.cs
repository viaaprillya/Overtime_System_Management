using API.ViewModels;
using API.Models;
using System.Collections.Generic;

namespace API.Repositories.Interface
{
    public interface ILemburRepository
    {
        List<Lembur> Get();
        Lembur Get(int id);
        List<Lembur> GetByKaryawanId(int karyawanid);
        int Post(PengajuanLembur pengajuanLembur);
        int Put(EditLembur lembur);
        int Delete(int id);
    }
}

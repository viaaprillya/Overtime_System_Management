using API.ViewModels;
using API.Models;
using System.Collections.Generic;

namespace API.Repositories.Interface
{
    public interface IKaryawanRepository
    {
        List<Karyawan> Get();
        Karyawan Get(int id);
        int Put(EditKaryawan karyawan);
        int Delete(int id);
    }
}

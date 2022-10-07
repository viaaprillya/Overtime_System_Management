using API.ViewModels;
using API.Models;
using System.Collections.Generic;

namespace API.Repositories.Interface
{
    public interface IJabatanRepository
    {
        List<Jabatan> Get();
        Jabatan Get(int id);
        int Post(InputJabatan inputJabatan);
        int Put(Jabatan jabatan);
        int Delete(int id);
    }
}

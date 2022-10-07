using API.Context;
using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JabatanController : ControllerBase
    {
        JabatanRepository jabatanRepository;
        public JabatanController(JabatanRepository jabatanRepository)
        {
            this.jabatanRepository = jabatanRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = jabatanRepository.Get();
            if (data != null)
                return Ok(new { message = "Berhasil Get Jabatan", statusCode = 200, data = data });
            return BadRequest(new { message = "Gagal Get Jabatan", statusCode = 400, data = data });
        }

        [HttpDelete]
        public IActionResult Delete(int ID)
        {
            var data = jabatanRepository.Delete(ID);
            if (data > 0)
                return Ok(new { message = "Berhasil Delete Jabatan", statusCode = 200});
            return BadRequest(new { message = "Gagal Delete Jabatan", statusCode = 400});
        }

        [HttpPost]
        public IActionResult Post(InputJabatan input)
        {
            var data = jabatanRepository.Post(input);
            if (data > 0)
                return Ok(new { message = "Berhasil Tambah Jabatan", statusCode = 200 });
            return BadRequest(new { message = "Gagal Tambah Jabatan", statusCode = 400 });
        }

        [HttpPut]
        public IActionResult Put(Jabatan jabatan)
        {
            var data = jabatanRepository.Put(jabatan);
            if (data > 0)
                return Ok(new { message = "Berhasil Edit Jabatan", statusCode = 200 });
            return BadRequest(new { message = "Gagal Edit Jabatan", statusCode = 400 });
        }
    }
}

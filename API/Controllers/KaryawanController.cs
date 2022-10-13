using API.Models;
using API.Repositories.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class KaryawanController : ControllerBase
    {
        KaryawanRepository karyawanRepository;
        public KaryawanController(KaryawanRepository karyawanRepository)
        {
            this.karyawanRepository = karyawanRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var data = karyawanRepository.Get();
            if (data != null)
                return Ok(new { message = "Berhasil Get Karyawan", statusCode = 200, data = data });
            return BadRequest(new { message = "Gagal Get Karyawan", statusCode = 400 });
        }

        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            var data = karyawanRepository.Get(id);
            if (data != null)
                return Ok(new { message = "Berhasil Get Karyawan", statusCode = 200, data = data });
            return BadRequest(new { message = "Gagal Get Karyawan", statusCode = 400 });
        }

        [HttpDelete("id")]
        public IActionResult Delete(int ID)
        {
            var data = karyawanRepository.Delete(ID);
            if (data > 0)
                return Ok(new { message = "Berhasil Delete Karyawan", statusCode = 200 });
            return BadRequest(new { message = "Gagal Delete Karyawan", statusCode = 400 });
        }

        [HttpPut]
        [Route("EditKaryawan")]
        public IActionResult Put(EditKaryawan karyawan)
        {
            var data = karyawanRepository.Put(karyawan);
            if (data > 0)
                return Ok(new { message = "Berhasil Edit Karyawan", statusCode = 200 });
            return BadRequest(new { message = "Gagal Edit Karyawan", statusCode = 400 });
        }
    }




}

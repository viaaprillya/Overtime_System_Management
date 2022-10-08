using API.Models;
using API.Repositories.Data;
using API.Repositories.Interface;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GajiController : ControllerBase
    {
        GajiRepository gajiRepository;

        public GajiController(GajiRepository gajiRepository)
        {
            this.gajiRepository = gajiRepository;
        }


        [HttpPost]
        [Route("CetakSlipGaji")]
        public IActionResult CetakSlipGaji([FromQuery] CetakSlipGaji cetak)
        {
            var data = gajiRepository.CetakSlipGaji(cetak);
            if (data != null)
                return Ok(new { message = "Berhasil Cetak Slip Gaji", statusCode = 200, data = data });
            return BadRequest(new { message = "Gagal Cetak Slip", statusCode = 400 });
        }

        [HttpGet]
        [Route("HistoryGaji")]
        public IActionResult HistoryGaji()
        {
            var data = gajiRepository.Get();
            if (data != null)
                return Ok(new { message = "Berhasil Cetak History Gaji", statusCode = 200, data = data });
            return BadRequest(new { message = "Gagal Cetak History Gaji", statusCode = 400 });
        }

        [HttpGet]
        [Route("HistoryGajiKaryawan")]
        public IActionResult HistoryGajiKaryawan([FromQuery] int idKaryawan)
        {
            var data = gajiRepository.Get(idKaryawan);
            if (data != null)
                return Ok(new { message = "Berhasil Cetak History Gaji Karyawan", statusCode = 200, data = data });
            return BadRequest(new { message = "Gagal Cetak History Gaji Karyawan", statusCode = 400, data = data });
        }
    }
}


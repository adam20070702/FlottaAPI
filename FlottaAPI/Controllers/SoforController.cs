using FlottaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 

namespace FlottaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoforController : ControllerBase
    {
        private FlottaContext _context;
        public SoforController(FlottaContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllSofor")]
        public async Task<IActionResult> GetAllsofor()
        {
            try
            {
                List<Sofor> lista = await _context.Sofors.ToListAsync();
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var talalat = _context.Sofors.Find(id);
                if (talalat == null)
                {
                    return NotFound("Nem található sofőr ezzel az azonosítóval: " + id);
                }
                return Ok(talalat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("SoforDelete/{id}")]
        public async Task<IActionResult> SoforDelete(int id)
        {
            try
            {
                Sofor sofordel = await _context.Sofors.FirstOrDefaultAsync(t => t.Id == id);

                if (sofordel == null)
                {
                    return NotFound("A megadott azonosítóval nem található sofőr.");
                }

                _context.Sofors.Remove(sofordel);
                await _context.SaveChangesAsync();
                return Ok("Sikeres törlés.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("PostSofor")]
        public IActionResult PostSofor( Sofor sofor)
        {
            try
            {
                _context.Sofors.Add(sofor);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("PutSofor")]
        public IActionResult PutSofor(Sofor sofor)
        {
            try
            {
                _context.Sofors.Update(sofor);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


    }
}
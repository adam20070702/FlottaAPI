using FlottaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlottaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GepjarmuController : ControllerBase
    {
        //controller
        private FlottaContext _context;
        public GepjarmuController(FlottaContext context) {
            _context = context;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try 
            {
                return Ok(_context.Gepjarmus.ToList());
            }
            catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetByRendszam/{rendszam}")]
        public IActionResult GetByRendszam(string rendszam)
        {
            try
            {
                var result = _context.Gepjarmus.FirstOrDefault(g => g.Rendszam == rendszam);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("UjGepjarmu")]
        public IActionResult UjGepjarmu(Gepjarmu gepjarmu)
        {
            try {
                if (gepjarmu is null)
                {
                    return BadRequest("Hiányzó adatok");
                }
                _context.Gepjarmus.Add(gepjarmu);
                _context.SaveChanges();
                return Ok("Sikeres felvitel");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("GetGepjarmuSofor/{id}")]
        public IActionResult GetGepjarmuSofor(int id)
        {
            try
            {
                if (_context.Gepjarmus.FirstOrDefault (g=>g.Id == id) is null)
                {
                    return NotFound("Hiányzó gépjármű");
                }
                List<Sofor> result = _context.Kikuldottjarmus.Include(k => k.SoforNavigation).Include(k => k.Gepjarmu).Where(b => b.GepjarmuId == id).Select(k => k.SoforNavigation).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        //[HttpPut("PutGepjarmu")]
        //public IActionResult PutGepjarmu(Gepjarmu gepjarmu)
        //{
        //    try
        //    {
        //        _context.Gepjarmus.Update(gepjarmu);
        //        _context.SaveChanges();
        //        return Ok("Sikeres mentés");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}

using FlottaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                _context.Gepjarmus.Add(gepjarmu);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("PutGepjarmu")]
        public IActionResult PutGepjarmu(Gepjarmu gepjarmu)
        {
            try
            {
                _context.Gepjarmus.Update(gepjarmu);
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

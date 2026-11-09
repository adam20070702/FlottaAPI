using FlottaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlottaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private FlottaContext _context;
        public UserController(FlottaContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllUser")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                List<User> lista = await _context.Users.ToListAsync();
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
                var talalat = _context.Users.Find(id);
                if (talalat == null)
                {
                    return NotFound("Nem található user ezzel az azonosítóval: " + id);
                }
                return Ok(talalat);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("UserDelete/{id}")]
        public async Task<IActionResult> UserDelete(int id)
        {
            try
            {
                User Userdel = await _context.Users.FirstOrDefaultAsync(t => t.Id == id);

                if (Userdel == null)
                {
                    return NotFound("A megadott azonosítóval nem található sofőr.");
                }

                _context.Users.Remove(Userdel);
                await _context.SaveChangesAsync();
                return Ok("Sikeres törlés.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("PostUser")]
        public IActionResult PostUser(User User)
        {
            try
            {
                _context.Users.Add(User);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("PutUser")]
        public IActionResult PutUser(User User)
        {
            try
            {
                _context.Users.Update(User);
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


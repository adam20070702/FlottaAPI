using Argon2Sharp;
using FlottaAPI.DTOs;
using FlottaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlottaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private FlottaContext _context;
        public LoginController(FlottaContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Login(LoginDTO logindto)
        {
            try {
                User user = _context.Users.FirstOrDefault(u => u.LoginName == logindto.LoginName);
                if (user == null)
                {
                    return NotFound("Missing user");
                }
                string phcstring = user.Hash;
                var (isValid, parameters) = Argon2Sharp.Argon2PhcFormat.VerifyPhcString(logindto.Password, phcstring);
                if (isValid)
                {
                    return Ok("token");
                }
                return BadRequest("Wrong password");
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

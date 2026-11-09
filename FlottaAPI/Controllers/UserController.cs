using FlottaAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_context.Users.ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Hiba: " + ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var talalat = _context.Users.FirstOrDefault(u => u.Id == id);
                if (talalat == null)
                {
                    return NotFound("Nincs ilyen azonosítójú sofőr");
                }
                return Ok(talalat);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Hiba: " + ex.Message);

            }
        }

        [HttpPost("Post")]
        public IActionResult Post(User ujUser)
        {
            try
            {
                _context.Users.Add(ujUser);
                _context.SaveChanges();
                return Ok("Sikeres mentés");
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Hiba: " + ex.Message);

            }
        }
        [HttpPut("Put")]
        public IActionResult Put(User ujUser)
        {
            try
            {
                var talalat = _context.Users.FirstOrDefault(u => u.Id == ujUser.Id);
                if (talalat == null)
                {
                    return NotFound("Nincs ilyen azonosítójú sofőr");

                }
                _context.ChangeTracker.Clear();
                _context.Update(talalat);
                _context.SaveChanges();
                return Ok("Sikeres mentés!");

            }
            catch (Exception ex)
            {

                return StatusCode(500, "Hiba: " + ex.Message);

            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var torlendo = _context.Users.FirstOrDefault(u => u.Id == id);
                if (torlendo == null)
                {
                    return NotFound("Nincs ilyen azonosítójú sofőr");

                }
                _context.ChangeTracker.Clear();
                _context.Remove(torlendo);
                _context.SaveChanges();
                return Ok("Sikeres törlés");
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Hiba: " + ex.Message);

            }
        }
    }
}


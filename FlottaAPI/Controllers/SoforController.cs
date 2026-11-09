using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlottaAPI.Models;
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

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_context.Sofors.ToList());
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
                var talalat = _context.Sofors.FirstOrDefault(s => s.Id == id);
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
        public IActionResult Post(Sofor ujSofor)
        {
            try
            {
                _context.Sofors.Add(ujSofor);
                _context.SaveChanges();
                return Ok("Sikeres mentés");
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Hiba: " + ex.Message);

            }
        }
        [HttpPut("Put")]
        public IActionResult Put(Sofor ujSofor)
        {
            try
            {
                var talalat= _context.Sofors.FirstOrDefault(s=> s.Id == ujSofor.Id);
                if(talalat == null)
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
                var torlendo = _context.Sofors.FirstOrDefault(s => s.Id == id);
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

        //adott sofor milyen jarmuveket vezetett?
        [HttpGet("SoforJarmu/{id}")]
        public IActionResult SoforJarmu(int id)
        {
            try
            {
                Sofor vane= _context.Sofors.FirstOrDefault(s =>s.Id == id);
                if(vane == null)
                {
                    return NotFound("Nincs ilyen sofőr");
                }
                var result=_context.Kikuldottjarmus.Where(j => j.SoforNavigation.Id==id).Select(j => j.Gepjarmu).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest("Hiba " + ex.Message);
            }
        }
    }
}

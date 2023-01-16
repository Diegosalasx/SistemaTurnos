using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ST.webAPI.Data;
using ST.webAPI.Data.Entities;

namespace ST.webAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorariosController : ControllerBase
    {
        private readonly TurnosMedicosContextdb _context;

        public HorariosController(TurnosMedicosContextdb context)
        {
            _context = context;
        }

        // GET: api/Horarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Horario>>> GetHorarios()
        {
          if (_context.Horarios == null)
          {
              return NotFound();
          }

          var result  = await _context.Horarios.Include(p => p.Dia).ToListAsync();
          return result;
        }

        // GET: api/Horarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Horario>> GetHorario(int id)
        {
          if (_context.Horarios == null)
          {
              return NotFound();
          }
            var horario = await _context.Horarios.FindAsync(id);

            if (horario == null)
            {
                return NotFound();
            }

            return horario;
        }

        // PUT: api/Horarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, Horario horario)
        {
            if (id != horario.HorarioId)
            {
                return BadRequest();
            }

            _context.Entry(horario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Horarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Horario>> PostHorario(Horario horario)
        {
          if (_context.Horarios == null)
          {
              return Problem("Entity set 'TurnosMedicosContextdb.Horarios'  is null.");
          }
            _context.Horarios.Add(horario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHorario", new { id = horario.HorarioId }, horario);
        }

        // DELETE: api/Horarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            if (_context.Horarios == null)
            {
                return NotFound();
            }
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HorarioExists(int id)
        {
            return (_context.Horarios?.Any(e => e.HorarioId == id)).GetValueOrDefault();
        }
    }
}

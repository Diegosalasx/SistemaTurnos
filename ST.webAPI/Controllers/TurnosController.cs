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
    public class TurnosController : ControllerBase
    {
        private readonly TurnosMedicosContextdb _context;

        public TurnosController(TurnosMedicosContextdb context)
        {
            _context = context;
        }

        // GET: api/Turnos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turno>>> GetTurnos()
        {
          if (_context.Turnos == null)
          {
              return NotFound();
          }
            return await _context.Turnos.Include(p=> p.Dia).Include(p=> p.EspecialidadMedica).ToListAsync();

        }

        // GET: api/Turnos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Turno>> GetTurno(int id)
        {
          if (_context.Turnos == null)
          {
              return NotFound();
          }

            var turno = await _context.Turnos
                    .Include(p => p.Dia)
                    .Include(p=> p.EspecialidadMedica)
                    .SingleOrDefaultAsync(p=> p.TurnoId == id);

            if (turno == null)
            {
                return NotFound();
            }

            return turno;

        }

        [HttpGet("GetTurnoExists")]
        public async Task<ActionResult<bool>> GetTurnoExists(int diaId, string desde, string hasta, int especialidadMedicaId, DateTime fecha)
        {
            var result = await _context.Turnos
                .Where(p=> p.DiaId == diaId 
                    && p.Desde == TimeSpan.Parse(desde)
                    && p.Hasta == TimeSpan.Parse(hasta)
                    && p.EspecialidadMedicaId == especialidadMedicaId
                    && p.Fecha == fecha)
                .AnyAsync();

            return Ok(result);
        }

        [HttpGet("GetTurnoByEspecialidadMedicaId/")]
        public async Task<ActionResult<IEnumerable<Turno>>> GetTurnoByEspecialidadMedicaId(int EspecialidadMedicaId)
        {
            if (_context.Turnos == null)
            {
                return Ok();
            }
            var turnos = await _context.Turnos.Include(p => p.Dia).Include(p => p.EspecialidadMedica)
                .Where(p => p.EspecialidadMedicaId == EspecialidadMedicaId).ToListAsync();

            if (turnos == null)
            {
                return Ok();
            }

            return turnos;
        }
        // PUT: api/Turnoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTurno(int id, int diaId, string desde, string hasta, int especialidadMedicaId, DateTime fecha, string pacienteNombre)
        {
            var item = await _context.Turnos.SingleOrDefaultAsync(p => p.TurnoId == id);
            item.DiaId = diaId;
            item.Desde = TimeSpan.Parse(desde);
            item.Hasta = TimeSpan.Parse(hasta);
            item.Fecha = fecha;
            item.EspecialidadMedicaId = especialidadMedicaId;
            item.PacienteNombre= pacienteNombre;

            _context.Turnos.Attach(item);
            _context.Turnos.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        // POST: api/Turnoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        //public async Task<ActionResult<Turno>> PostTurno(Turno turno)
        public async Task<ActionResult<Turno>> PostTurno(int diaId, string desde, string hasta, int especialidadMedicaId, DateTime fecha, string pacienteNombre)
        {
            //if (_context.Turnos == null)
            //{
            //    return Problem("Entity set 'TurnosMedicosContextdb.Turnos'  is null.");
            //}
            var turno = new Turno()
            {
                DiaId = diaId,
                EspecialidadMedicaId = especialidadMedicaId,
                Desde = TimeSpan.Parse(desde),
                Hasta = TimeSpan.Parse(hasta),
                Fecha = fecha,
                 PacienteNombre= pacienteNombre

            };

            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();

            return Ok(turno);
        }

        // DELETE: api/Turnoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTurno(int id)
        {
            if (_context.Turnos == null)
            {
                return NotFound();
            }
            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
            {
                return NotFound();
            }

            _context.Turnos.Remove(turno);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TurnoExists(int id)
        {
            return (_context.Turnos?.Any(e => e.TurnoId == id)).GetValueOrDefault();
        }
    }
}

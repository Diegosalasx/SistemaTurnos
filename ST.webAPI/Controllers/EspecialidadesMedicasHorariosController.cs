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
    public class EspecialidadesMedicasHorariosController : ControllerBase
    {
        private readonly TurnosMedicosContextdb _context;

        public EspecialidadesMedicasHorariosController(TurnosMedicosContextdb context)
        {
            _context = context;
        }

        // GET: api/EspecialidadesMedicasHorarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadesMedicasHorario>>> GetEspecialidadesMedicasHorarios()
        {
            return await _context.EspecialidadesMedicasHorarios.ToListAsync();
        }

        // GET: api/EspecialidadesMedicasHorarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadesMedicasHorario>> GetEspecialidadesMedicasHorario(int id)
        {
            var especialidadesMedicasHorario = await _context.EspecialidadesMedicasHorarios.FindAsync(id);

            if (especialidadesMedicasHorario == null)
            {
                return NotFound();
            }

            return especialidadesMedicasHorario;
        }

        // GET: api/EspecialidadesMedicasHorarios/5
        [HttpGet("GetEspecialidadesMedicasHorarioById/{id}")]
        public async Task<ActionResult<IEnumerable<EspecialidadesMedicasHorario>>> GetEspecialidadesMedicasHorarioById(int id)
        {
            IEnumerable<EspecialidadesMedicasHorario> result =
                await _context.EspecialidadesMedicasHorarios
                            .Include(p=> p.Horario)
                            .Include(p=> p.Horario.Dia)
                            .Where(p => p.EspecialidadMedicaId == id)
                            .ToListAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        // PUT: api/EspecialidadesMedicasHorarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidadesMedicasHorario(int id, EspecialidadesMedicasHorario especialidadesMedicasHorario)
        {
            if (id != especialidadesMedicasHorario.EspecialidadMedicaHorarioId)
            {
                return BadRequest();
            }

            _context.Entry(especialidadesMedicasHorario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecialidadesMedicasHorarioExists(id))
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

        // POST: api/EspecialidadesMedicasHorarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EspecialidadesMedicasHorario>> PostEspecialidadesMedicasHorario(EspecialidadesMedicasHorario especialidadesMedicasHorario)
        {
            _context.EspecialidadesMedicasHorarios.Add(especialidadesMedicasHorario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEspecialidadesMedicasHorario", new { id = especialidadesMedicasHorario.EspecialidadMedicaHorarioId }, especialidadesMedicasHorario);
        }

        [HttpPost("PostEspecialoidadesMedicasAndHorarios")]
        public async Task<ActionResult> PostEspecialoidadesMedicasAndHorarios(int EspecialidadMedicaId, int[] Horarios) 
        {
            foreach(var horario in Horarios)
            {
                _context.EspecialidadesMedicasHorarios.Add(
                    new EspecialidadesMedicasHorario(){
                         EspecialidadMedicaId = EspecialidadMedicaId, 
                        HorarioId = horario
                    });
            
            }

            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/EspecialidadesMedicasHorarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidadesMedicasHorario(int id)
        {
            var especialidadesMedicasHorario = await _context.EspecialidadesMedicasHorarios.FindAsync(id);
            if (especialidadesMedicasHorario == null)
            {
                return NotFound();
            }

            _context.EspecialidadesMedicasHorarios.Remove(especialidadesMedicasHorario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("DeleteEspecialidadesMedicasHorarioByEspecialidadMedicaId/{id}")]
        public async Task<IActionResult> DeleteEspecialidadesMedicasHorarioByEspecialidadMedicaId(int id)
        {
     
            _context.EspecialidadesMedicasHorarios.RemoveRange(_context.EspecialidadesMedicasHorarios.Where(p => p.EspecialidadMedicaId == id));
            await _context.SaveChangesAsync();

            return Ok();
        }
        private bool EspecialidadesMedicasHorarioExists(int id)
        {
            return _context.EspecialidadesMedicasHorarios.Any(e => e.EspecialidadMedicaHorarioId == id);
        }
    }
}

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
    public class EspecialidadesMedicasController : ControllerBase
    {
        private readonly TurnosMedicosContextdb _context;

        public EspecialidadesMedicasController(TurnosMedicosContextdb context)
        {
            _context = context;
        }

        // GET: api/EspecialidadesMedicas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadesMedica>>> GetEspecialidadesMedicas()
        {
            return await _context.EspecialidadesMedicas.ToListAsync();
        }

        // GET: api/EspecialidadesMedicas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadesMedica>> GetEspecialidadesMedica(int id)
        {
            var especialidadesMedica = await _context.EspecialidadesMedicas.FindAsync(id);

            if (especialidadesMedica == null)
            {
                return NotFound();
            }

            return especialidadesMedica;
        }

        // PUT: api/EspecialidadesMedicas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEspecialidadesMedica(int id, EspecialidadesMedica especialidadesMedica)
        {
            if (id != especialidadesMedica.EspecialidadMedicaId)
            {
                return BadRequest();
            }

            _context.Entry(especialidadesMedica).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EspecialidadesMedicaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/EspecialidadesMedicas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EspecialidadesMedica>> PostEspecialidadesMedica(EspecialidadesMedica especialidadesMedica)
        {
            _context.EspecialidadesMedicas.Add(especialidadesMedica);
            await _context.SaveChangesAsync();

            return Ok(especialidadesMedica);
            //return CreatedAtAction("GetEspecialidadesMedica", new { id = especialidadesMedica.EspecialidadMedicaId }, especialidadesMedica);

        }

        // DELETE: api/EspecialidadesMedicas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidadesMedica(int id)
        {
            var especialidadesMedica = await _context.EspecialidadesMedicas.FindAsync(id);
            if (especialidadesMedica == null)
            {
                return NotFound();
            }

            _context.EspecialidadesMedicas.Remove(especialidadesMedica);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool EspecialidadesMedicaExists(int id)
        {
            return _context.EspecialidadesMedicas.Any(e => e.EspecialidadMedicaId == id);
        }
    }
}

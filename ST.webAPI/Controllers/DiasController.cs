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
    public class DiasController : ControllerBase
    {
        private readonly TurnosMedicosContextdb _context;

        public DiasController(TurnosMedicosContextdb context)
        {
            _context = context;
        }

        // GET: api/Dias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dia>>> GetDias()
        {
            return await _context.Dias.ToListAsync();
        }

        // GET: api/Dias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dia>> GetDia(int id)
        {
            var dia = await _context.Dias.FindAsync(id);

            if (dia == null)
            {
                return NotFound();
            }

            return dia;
        }

        // PUT: api/Dias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDia(int id, Dia dia)
        {
            if (id != dia.DiaId)
            {
                return BadRequest();
            }

            _context.Entry(dia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiaExists(id))
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

        // POST: api/Dias
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dia>> PostDia(Dia dia)
        {
            _context.Dias.Add(dia);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDia", new { id = dia.DiaId }, dia);
        }

        // DELETE: api/Dias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDia(int id)
        {
            var dia = await _context.Dias.FindAsync(id);
            if (dia == null)
            {
                return NotFound();
            }

            _context.Dias.Remove(dia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiaExists(int id)
        {
            return _context.Dias.Any(e => e.DiaId == id);
        }
    }
}

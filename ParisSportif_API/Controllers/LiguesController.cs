using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ParisSportif_API.Data;
using ProjectFootAPI.Model;

namespace ParisSportif_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LiguesController : ControllerBase
    {
        private readonly ParisSportifContext _context;

        public LiguesController(ParisSportifContext context)
        {
            _context = context;
        }

        // GET: api/Ligues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ligue>>> GetLigues()
        {
            return await _context.Ligues.ToListAsync();
        }

        // GET: api/Ligues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ligue>> GetLigue(int id)
        {
            var ligue = await _context.Ligues.FindAsync(id);

            if (ligue == null)
            {
                return NotFound();
            }

            return ligue;
        }

        // PUT: api/Ligues/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLigue(int id, Ligue ligue)
        {
            if (id != ligue.Id)
            {
                return BadRequest();
            }

            _context.Entry(ligue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LigueExists(id))
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

        // POST: api/Ligues
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ligue>> PostLigue(Ligue ligue)
        {
            _context.Ligues.Add(ligue);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLigue", new { id = ligue.Id }, ligue);
        }

        // DELETE: api/Ligues/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLigue(int id)
        {
            var ligue = await _context.Ligues.FindAsync(id);
            if (ligue == null)
            {
                return NotFound();
            }

            _context.Ligues.Remove(ligue);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LigueExists(int id)
        {
            return _context.Ligues.Any(e => e.Id == id);
        }
    }
}

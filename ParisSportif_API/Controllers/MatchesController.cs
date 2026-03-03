using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using ParisSportif_API.Data;
using ProjectFootAPI.Model;

namespace ParisSportif_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly ParisSportifContext _context;

        public MatchesController(ParisSportifContext context)
        {
            _context = context;
        }

        // GET: api/Matches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatches()
        {
            return await _context.Matches
                .Include(m => m.Club1)
                .Include(m => m.Club2)
                .Include(m => m.Club1.Ligue)
                .ToListAsync();
        }

        // GET: api/Matches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        // GET: api/Matches/club/5
        [HttpGet("club/{id}")]
        public ActionResult<List<Match>> GetClubMatches(int id)
        {
            List<Match> matches = (List<Match>)[.. _context.Matches.Where(m => m.ClubId1 == id || m.ClubId2 == id)];

            if (matches == null || matches.Count() == 0)
                return NotFound();

            return matches;
        }

        // PUT: api/Matches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, Match match)
        {
            if (id != match.Id)
            {
                return BadRequest();
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // POST: api/Matches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch([FromBody] Match match)
        {
            try
            {
                if (match.ClubId1 <= 0 || match.ClubId2 <= 0)
                    return BadRequest("ClubId1 et ClubId2 doivent être > 0.");

                if (match.ClubId1 == match.ClubId2)
                    return BadRequest("Club1 et Club2 doivent être différents.");

                var club1Exists = await _context.Clubs.AnyAsync(c => c.Id == match.ClubId1);
                var club2Exists = await _context.Clubs.AnyAsync(c => c.Id == match.ClubId2);

                if (!club1Exists || !club2Exists)
                    return BadRequest("Un des clubs n'existe pas.");

                _context.Matches.Add(match);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetMatch", new { id = match.Id }, match);
            }
            catch (Exception ex)
            {
                // en DEV, affiche l'erreur réelle
                return StatusCode(500, "Erreur interne du serveur : " + ex.Message);
            }
        }


        // DELETE: api/Matches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Matches.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchExists(int id)
        {
            return _context.Matches.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportPal.Data;
using SportPal.Models;

namespace SportPal.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StandingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Standings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Standing>>> GetStanding()
        {
            return await _context.Standing.ToListAsync();
        }

        // GET: api/Standings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Standing>> GetStanding(int id)
        {
            var standing = await _context.Standing.FindAsync(id);

            if (standing == null)
            {
                return NotFound();
            }

            return standing;
        }

        // PUT: api/Standings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStanding(int id, Standing standing)
        {
            if (id != standing.StandingId)
            {
                return BadRequest();
            }

            _context.Entry(standing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StandingExists(id))
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

        // POST: api/Standings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Standing>> PostStanding(Standing standing)
        {
            _context.Standing.Add(standing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStanding", new { id = standing.StandingId }, standing);
        }

        // DELETE: api/Standings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStanding(int id)
        {
            var standing = await _context.Standing.FindAsync(id);
            if (standing == null)
            {
                return NotFound();
            }

            _context.Standing.Remove(standing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StandingExists(int id)
        {
            return _context.Standing.Any(e => e.StandingId == id);
        }
    }
}

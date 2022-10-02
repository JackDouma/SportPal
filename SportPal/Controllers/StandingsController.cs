using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportPal.Data;
using SportPal.Models;

namespace SportPal.Controllers
{
    public class StandingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StandingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Standings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.League.Include(l => l.LeagueId);
            return View(await _context.Standings.ToListAsync());
        }

        // GET: Standings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Standings == null)
            {
                return NotFound();
            }

            var standings = await _context.Standings
                .Include(l => l.League)
                .FirstOrDefaultAsync(m => m.StandingsId == id);
            if (standings == null)
            {
                return NotFound();
            }

            return View(standings);
        }

        // GET: Standings/Create
        public IActionResult Create()
        {
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "Name");
            return View();
        }

        // POST: Standings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StandingsId,Team,Coach,Wins,Losses,Ties,LeagueId")] Standings standings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(standings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "Name", standings.LeagueId);
            return View(standings);
        }

        // GET: Standings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Standings == null)
            {
                return NotFound();
            }

            var standings = await _context.Standings.FindAsync(id);
            if (standings == null)
            {
                return NotFound();
            }
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "Name", standings.LeagueId);
            return View(standings);
        }

        // POST: Standings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StandingsId,Team,Coach,Wins,Losses,Ties,LeagueId")] Standings standings)
        {
            if (id != standings.StandingsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(standings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandingsExists(standings.StandingsId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LeagueId"] = new SelectList(_context.League, "LeagueId", "Name", standings.LeagueId);
            return View(standings);
        }

        // GET: Standings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Standings == null)
            {
                return NotFound();
            }

            var standings = await _context.Standings
                .Include(l => l.League)
                .FirstOrDefaultAsync(m => m.StandingsId == id);
            if (standings == null)
            {
                return NotFound();
            }

            return View(standings);
        }

        // POST: Standings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Standings == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Standings'  is null.");
            }
            var standings = await _context.Standings.FindAsync(id);
            if (standings != null)
            {
                _context.Standings.Remove(standings);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StandingsExists(int id)
        {
            return _context.Standings.Any(e => e.StandingsId == id);
        }
    }
}

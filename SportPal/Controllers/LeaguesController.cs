using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportPal.Data;
using SportPal.Models;

namespace SportPal.Controllers
{
    [Authorize]
    public class LeaguesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaguesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: Leagues
        public async Task<IActionResult> Index()
        {
              return View(await _context.Leagues.ToListAsync());
        }
        [AllowAnonymous]
        // GET: Leagues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Leagues == null)
            {
                return NotFound();
            }

            var league = await _context.Leagues
                .FirstOrDefaultAsync(m => m.LeagueId == id);
            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }

        // GET: Leagues/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Leagues/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LeagueId,Name,Sport,Organizer")] League league)
        {
            if (ModelState.IsValid)
            {
                _context.Add(league);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(league);
        }

        // GET: Leagues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Leagues == null)
            {
                return NotFound();
            }

            var league = await _context.Leagues.FindAsync(id);
            if (league == null)
            {
                return NotFound();
            }
            return View(league);
        }

        // POST: Leagues/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LeagueId,Name,Sport,Organizer")] League league)
        {
            if (id != league.LeagueId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(league);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeagueExists(league.LeagueId))
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
            return View(league);
        }

        // GET: Leagues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Leagues == null)
            {
                return NotFound();
            }

            var league = await _context.Leagues
                .FirstOrDefaultAsync(m => m.LeagueId == id);
            if (league == null)
            {
                return NotFound();
            }

            return View(league);
        }

        // POST: Leagues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Leagues == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Leagues'  is null.");
            }
            var league = await _context.Leagues.FindAsync(id);
            if (league != null)
            {
                _context.Leagues.Remove(league);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeagueExists(int id)
        {
          return _context.Leagues.Any(e => e.LeagueId == id);
        }
    }
}

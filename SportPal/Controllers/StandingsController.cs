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
    public class StandingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StandingsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        // GET: Standings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Standing.Include(s => s.League);
            return View("Index", await applicationDbContext.OrderByDescending(s => s.Points).ToListAsync());
        }
        [AllowAnonymous]
        // GET: Standings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Standing == null)
            {
                return View("404");
            }

            var standing = await _context.Standing
                .Include(s => s.League)
                .FirstOrDefaultAsync(m => m.StandingId == id);
            if (standing == null)
            {
                return View("404");
            }

            return View("Details", standing);
        }

        // GET: Standings/Create
        public IActionResult Create()
        {
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "Name");
            return View("Create");
        }

        // POST: Standings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StandingId,Team,Coach,Wins,Losses,Ties,LeagueId")] Standing standing)
        {
            if (ModelState.IsValid)
            {
                // set points based on wins and ties
                standing.Points = (standing.Wins * 2) + (standing.Ties);

                _context.Add(standing);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "Name", standing.LeagueId);
            return View("Create", standing);
        }

        // GET: Standings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Standing == null)
            {
                return View("404");
            }

            var standing = await _context.Standing.FindAsync(id);
            if (standing == null)
            {
                return View("404");
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "Name", standing.LeagueId);
            return View("Edit", standing);
        }

        // POST: Standings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StandingId,Team,Coach,Points,Wins,Losses,Ties,LeagueId")] Standing standing)
        {
            if (id != standing.StandingId)
            {
                return View("404");
            }

            if (ModelState.IsValid)
            {
                // set points based on wins and ties
                standing.Points = (standing.Wins * 2) + (standing.Ties);

                try
                {
                    _context.Update(standing);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StandingExists(standing.StandingId))
                    {
                        return View("404");
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["LeagueId"] = new SelectList(_context.Leagues, "LeagueId", "Name", standing.LeagueId);
            return View("Edit", standing);
        }

        // GET: Standings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Standing == null)
            {
                return View("404");
            }

            var standing = await _context.Standing
                .Include(s => s.League)
                .FirstOrDefaultAsync(m => m.StandingId == id);
            if (standing == null)
            {
                return View("404");
            }

            return View("Delete", standing);
        }

        // POST: Standings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Standing == null)
            {
                return View("404");
            }
            var standing = await _context.Standing.FindAsync(id);
            if (standing != null)
            {
                _context.Standing.Remove(standing);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StandingExists(int id)
        {
          return _context.Standing.Any(e => e.StandingId == id);
        }
    }
}

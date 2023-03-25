using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugTracker.Model.Database;

namespace BugTracker.Controllers
{
    public class MitarbeiterProjektsController : Controller
    {
        private readonly BugTrackerContext _context;

        public MitarbeiterProjektsController(BugTrackerContext context)
        {
            _context = context;
        }

        // GET: MitarbeiterProjekts
        public async Task<IActionResult> Index()
        {
            var bugTrackerContext = _context.MitarbeiterProjekts.Include(m => m.Mitarbeiter).Include(m => m.Projekt);
            return View(await bugTrackerContext.ToListAsync());
        }

        // GET: MitarbeiterProjekts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mitarbeiterProjekt = await _context.MitarbeiterProjekts
                .Include(m => m.Mitarbeiter)
                .Include(m => m.Projekt)
                .FirstOrDefaultAsync(m => m.MitarbeiterProjektId == id);
            if (mitarbeiterProjekt == null)
            {
                return NotFound();
            }

            return View(mitarbeiterProjekt);
        }

        // GET: MitarbeiterProjekts/Create
        public IActionResult Create()
        {
            ViewData["MitarbeiterId"] = new SelectList(_context.Mitarbeiters.Select(m => new { m.MitarbeiterId, Name = m.Vorname +" " + m.Nachname }), "MitarbeiterId", "Name");
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "Projektname");
            return View();
        }

        // POST: MitarbeiterProjekts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MitarbeiterProjektId,MitarbeiterId,ProjektId")] MitarbeiterProjekt mitarbeiterProjekt)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mitarbeiterProjekt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MitarbeiterId"] = new SelectList(_context.Mitarbeiters.Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }), "MitarbeiterId", "Name", mitarbeiterProjekt.MitarbeiterId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "Projektname", mitarbeiterProjekt.ProjektId);
            return View(mitarbeiterProjekt);
        }

        // GET: MitarbeiterProjekts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mitarbeiterProjekt = await _context.MitarbeiterProjekts.FindAsync(id);
            if (mitarbeiterProjekt == null)
            {
                return NotFound();
            }
            ViewData["MitarbeiterId"] = new SelectList(_context.Mitarbeiters.Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }), "MitarbeiterId", "Name", mitarbeiterProjekt.MitarbeiterId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "Projektname", mitarbeiterProjekt.ProjektId);
            return View(mitarbeiterProjekt);
        }

        // POST: MitarbeiterProjekts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MitarbeiterProjektId,MitarbeiterId,ProjektId")] MitarbeiterProjekt mitarbeiterProjekt)
        {
            if (id != mitarbeiterProjekt.MitarbeiterProjektId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mitarbeiterProjekt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MitarbeiterProjektExists(mitarbeiterProjekt.MitarbeiterProjektId))
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
            ViewData["MitarbeiterId"] = new SelectList(_context.Mitarbeiters.Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }), "MitarbeiterId", "Name", mitarbeiterProjekt.MitarbeiterId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "Projektname", mitarbeiterProjekt.ProjektId);
            return View(mitarbeiterProjekt);
        }

        // GET: MitarbeiterProjekts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mitarbeiterProjekt = await _context.MitarbeiterProjekts
                .Include(m => m.Mitarbeiter)
                .Include(m => m.Projekt)
                .FirstOrDefaultAsync(m => m.MitarbeiterProjektId == id);
            if (mitarbeiterProjekt == null)
            {
                return NotFound();
            }

            return View(mitarbeiterProjekt);
        }

        // POST: MitarbeiterProjekts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mitarbeiterProjekt = await _context.MitarbeiterProjekts.FindAsync(id);
            _context.MitarbeiterProjekts.Remove(mitarbeiterProjekt);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MitarbeiterProjektExists(int id)
        {
            return _context.MitarbeiterProjekts.Any(e => e.MitarbeiterProjektId == id);
        }
    }
}

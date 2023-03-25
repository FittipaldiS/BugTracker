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
    public class BugsController : Controller
    {
        private readonly BugTrackerContext _context;

        public BugsController(BugTrackerContext context)
        {
            _context = context;
        }

        // GET: Bugs
        public async Task<IActionResult> Index()
        {
            var bugTrackerContext = _context.Bugs.Include(b => b.MitarbeiterDev).Include(b => b.MitarbeiterTest).Include(b => b.Projekt);
            return View(await bugTrackerContext.ToListAsync());
        }

        // GET: Bugs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs
                .Include(b => b.MitarbeiterDev)
                .Include(b => b.MitarbeiterTest)
                .Include(b => b.Projekt)
                .FirstOrDefaultAsync(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // GET: Bugs/Create
        public IActionResult Create()
        {
            ViewData["MitarbeiterDevId"] = new SelectList(_context.Mitarbeiters.Where(m => m.Abteilung == "Dev").Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }), "MitarbeiterId", "Name");
            ViewData["MitarbeiterTestId"] = new SelectList(_context.Mitarbeiters.Where(m => m.Abteilung == "Test").Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }), "MitarbeiterId", "Name");
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "Projektname");
            return View();
        }

        // POST: Bugs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BugId,ProjektId,MitarbeiterTestId,MitarbeiterDevId,Titel,Beschreibung,Anlagedatum")] Bug bug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MitarbeiterDevId"] = new SelectList(_context.Mitarbeiters, "MitarbeiterId", "Nachname", bug.MitarbeiterDevId);
            ViewData["MitarbeiterTestId"] = new SelectList(_context.Mitarbeiters, "MitarbeiterId", "Nachname", bug.MitarbeiterTestId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "Projektname", bug.ProjektId);
            return View(bug);
        }

        // GET: Bugs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs.FindAsync(id);
            if (bug == null)
            {
                return NotFound();
            }
            ViewData["MitarbeiterDevId"] = new SelectList(_context.Mitarbeiters.Where(m => m.Abteilung == "Dev").Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }), "MitarbeiterId", "Name", bug.MitarbeiterDevId);
            ViewData["MitarbeiterTestId"] = new SelectList(_context.Mitarbeiters.Where(m => m.Abteilung == "Test").Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }), "MitarbeiterId", "Name", bug.MitarbeiterTestId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "Projektname", bug.ProjektId);
            return View(bug);
        }

        // POST: Bugs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BugId,ProjektId,MitarbeiterTestId,MitarbeiterDevId,Titel,Beschreibung,Anlagedatum")] Bug bug)
        {
            if (id != bug.BugId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bug);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BugExists(bug.BugId))
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
            ViewData["MitarbeiterDevId"] = new SelectList(_context.Mitarbeiters, "MitarbeiterId", "Nachname", bug.MitarbeiterDevId);
            ViewData["MitarbeiterTestId"] = new SelectList(_context.Mitarbeiters, "MitarbeiterId", "Nachname", bug.MitarbeiterTestId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "Projektname", bug.ProjektId);
            return View(bug);
        }

        // GET: Bugs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = await _context.Bugs
                .Include(b => b.MitarbeiterDev)
                .Include(b => b.MitarbeiterTest)
                .Include(b => b.Projekt)
                .FirstOrDefaultAsync(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        // POST: Bugs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bug = await _context.Bugs.FindAsync(id);
            _context.Bugs.Remove(bug);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BugExists(int id)
        {
            return _context.Bugs.Any(e => e.BugId == id);
        }

        // GET: Mitarbeiters/Details/5
        public async Task<IActionResult> BugsMaDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mitarbeiter = await _context.Mitarbeiters
                .FirstOrDefaultAsync(m => m.MitarbeiterId == id);
            if (mitarbeiter == null)
            {
                return NotFound();
            }

            return View(mitarbeiter);
        }
    }
}

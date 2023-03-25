using BugTracker.Model.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {

        private readonly BugTrackerContext _context;

        public HomeController(BugTrackerContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DevBugList()
        {
            List<Bug> devList = _context.Bugs.Include(b => b.MitarbeiterDev)
                                             .Include(b => b.MitarbeiterTest)
                                             .Include(b => b.Projekt)
                                             .ToList();
            return View(devList);
        }
        public IActionResult TesterBugList()
        {
            List<Bug> testerList = _context.Bugs.Include(b => b.MitarbeiterDev)
                                                .Include(b => b.MitarbeiterTest)
                                                .Include(b => b.Projekt)
                                                .ToList();
            return View(testerList);
        }

        public IActionResult DevDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = _context.Bugs
                .Include(b => b.MitarbeiterDev)
                .Include(b => b.MitarbeiterTest)
                .Include(b => b.Projekt)
                .FirstOrDefault(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        public IActionResult TesterDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bug = _context.Bugs
                .Include(b => b.MitarbeiterDev)
                .Include(b => b.MitarbeiterTest)
                .Include(b => b.Projekt)
                .FirstOrDefault(m => m.BugId == id);
            if (bug == null)
            {
                return NotFound();
            }

            return View(bug);
        }

        public IActionResult TesterEdit(int? id)
        {
            var bug = _context.Bugs.Include(b => b.MitarbeiterDev)
                .Include(b => b.MitarbeiterTest)
                .Include(b => b.Projekt)
                .FirstOrDefault(m => m.BugId == id);
            ViewData["MitarbeiterDevId"] = new SelectList(_context.Mitarbeiters.Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }), "MitarbeiterId", "Name", bug.MitarbeiterDevId);
            ViewData["MitarbeiterTestId"] = new SelectList(_context.Mitarbeiters.Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }), "MitarbeiterId", "Name", bug.MitarbeiterTestId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "Projektname", bug.ProjektId);
            return View(bug);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TesterEdit(int id, [Bind("BugId,ProjektId,MitarbeiterTestId,MitarbeiterDevId,Titel,Beschreibung,Anlagedatum")] Bug bug)
        {
            if (id != bug.BugId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                    _context.Update(bug);
                    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(TesterBugList));
            }

            ViewData["MitarbeiterDevId"] = new SelectList(_context.Mitarbeiters.Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }), "MitarbeiterId", "Name", bug.MitarbeiterDevId);
            ViewData["MitarbeiterTestId"] = new SelectList(_context.Mitarbeiters.Select(m => new { m.MitarbeiterId, Name = m.Vorname + " " + m.Nachname }), "MitarbeiterId", "Name", bug.MitarbeiterTestId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "Projektname", bug.ProjektId);
            return View(bug);
        }

        // GET: Bugs/Create
        public IActionResult TesterCreate(int? id)
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
        public async Task<IActionResult> TesterCreate([Bind("BugId,ProjektId,MitarbeiterTestId,MitarbeiterDevId,Titel,Beschreibung,Anlagedatum")] Bug bug)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bug);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TesterBugList));
            }
            ViewData["MitarbeiterDevId"] = new SelectList(_context.Mitarbeiters, "MitarbeiterId", "Nachname", bug.MitarbeiterDevId);
            ViewData["MitarbeiterTestId"] = new SelectList(_context.Mitarbeiters, "MitarbeiterId", "Nachname", bug.MitarbeiterTestId);
            ViewData["ProjektId"] = new SelectList(_context.Projekts, "ProjektId", "Projektname", bug.ProjektId);
            return View(bug);
        }

        // GET: Mitarbeiters/Details/5
        public async Task<IActionResult> DevMaDetails(int? id)
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

        // GET: Mitarbeiters/Details/5
        public async Task<IActionResult> TesterMaDetails(int? id)
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

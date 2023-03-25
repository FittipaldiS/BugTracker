using BugTracker.Model.Database;
using BugTracker.Model.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Controllers
{
    public class AdminController : Controller
    {
        private readonly BugTrackerContext _context;

        public AdminController(BugTrackerContext context)
        {
            _context = context;
        }

        public ActionResult Index(string? searchString)
        {
            AdminIndexViewModel indexView = new AdminIndexViewModel();

            indexView.Mitarbeiterliste = _context.Mitarbeiters.ToList();
            indexView.Projektliste = _context.Projekts.ToList();
            //indexView.Bugliste = _context.Bugs.ToList();
            indexView.Bugliste = _context.Bugs.Where(m => m.MitarbeiterDev != null).ToList();
            indexView.BuglisteOhne = _context.Bugs.Where(m => m.MitarbeiterDev == null).ToList();
            indexView.Abteilung = _context.Mitarbeiters.Select(m => m.Abteilung).Distinct().ToList();

            if (searchString != null)
            {
                indexView.Mitarbeiterliste = _context.Mitarbeiters.Where(m => m.Vorname
                                                                  .Contains(searchString) || m.Nachname
                                                                  .Contains(searchString))
                                                                  .OrderBy(m => m.MitarbeiterId)
                                                                  .ToList();
            }
            return View(indexView);
        }
    }
}

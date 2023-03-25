using BugTracker.Model.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Model.ViewModels
{
    public class AdminIndexViewModel
    {
        public List<Mitarbeiter> Mitarbeiterliste { get; set; }
        public List<Projekt> Projektliste { get; set; }
        public List<Bug> Bugliste { get; set; }
        public List<string> Abteilung { get; set; }

        public List<Bug> BuglisteOhne { get; set; }
    }
}

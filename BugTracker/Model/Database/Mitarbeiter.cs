using System;
using System.Collections.Generic;

#nullable disable

namespace BugTracker.Model.Database
{
    public partial class Mitarbeiter
    {
        public Mitarbeiter()
        {
            BugMitarbeiterDevs = new HashSet<Bug>();
            BugMitarbeiterTests = new HashSet<Bug>();
            MitarbeiterProjekts = new HashSet<MitarbeiterProjekt>();
        }

        public int MitarbeiterId { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string Abteilung { get; set; }

        public virtual ICollection<Bug> BugMitarbeiterDevs { get; set; }
        public virtual ICollection<Bug> BugMitarbeiterTests { get; set; }
        public virtual ICollection<MitarbeiterProjekt> MitarbeiterProjekts { get; set; }
    }
}

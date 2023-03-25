using System;
using System.Collections.Generic;

#nullable disable

namespace BugTracker.Model.Database
{
    public partial class Bug
    {
        public int BugId { get; set; }
        public string Titel { get; set; }
        public string Beschreibung { get; set; }
        public DateTime Anlagedatum { get; set; }
        public int? ProjektId { get; set; }
        public int? MitarbeiterTestId { get; set; }
        public int? MitarbeiterDevId { get; set; }

        public virtual Mitarbeiter MitarbeiterDev { get; set; }
        public virtual Mitarbeiter MitarbeiterTest { get; set; }
        public virtual Projekt Projekt { get; set; }
    }
}

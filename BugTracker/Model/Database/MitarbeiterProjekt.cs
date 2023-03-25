using System;
using System.Collections.Generic;

#nullable disable

namespace BugTracker.Model.Database
{
    public partial class MitarbeiterProjekt
    {
        public int MitarbeiterProjektId { get; set; }
        public int? MitarbeiterId { get; set; }
        public int? ProjektId { get; set; }

        public virtual Mitarbeiter Mitarbeiter { get; set; }
        public virtual Projekt Projekt { get; set; }
    }
}

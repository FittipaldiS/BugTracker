using System;
using System.Collections.Generic;

#nullable disable

namespace BugTracker.Model.Database
{
    public partial class Projekt
    {
        public Projekt()
        {
            Bugs = new HashSet<Bug>();
            MitarbeiterProjekts = new HashSet<MitarbeiterProjekt>();
        }

        public int ProjektId { get; set; }
        public string Projektname { get; set; }
        public DateTime Startdatum { get; set; }
        public DateTime Enddatum { get; set; }

        public virtual ICollection<Bug> Bugs { get; set; }
        public virtual ICollection<MitarbeiterProjekt> MitarbeiterProjekts { get; set; }
    }
}

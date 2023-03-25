using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace BugTracker.Model.Database
{
    public partial class BugTrackerContext : DbContext
    {
        public BugTrackerContext()
        {
        }

        public BugTrackerContext(DbContextOptions<BugTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bug> Bugs { get; set; }
        public virtual DbSet<Mitarbeiter> Mitarbeiters { get; set; }
        public virtual DbSet<MitarbeiterProjekt> MitarbeiterProjekts { get; set; }
        public virtual DbSet<Projekt> Projekts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB; database=BugTracker; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Bug>(entity =>
            {
                entity.ToTable("Bug");

                entity.Property(e => e.BugId).HasColumnName("BugID");

                entity.Property(e => e.Beschreibung)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.MitarbeiterDevId).HasColumnName("MitarbeiterDevID");

                entity.Property(e => e.MitarbeiterTestId).HasColumnName("MitarbeiterTestID");

                entity.Property(e => e.ProjektId).HasColumnName("ProjektID");

                entity.Property(e => e.Titel)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.MitarbeiterDev)
                    .WithMany(p => p.BugMitarbeiterDevs)
                    .HasForeignKey(d => d.MitarbeiterDevId)
                    .HasConstraintName("FK__Bug__Mitarbeiter__2D27B809");

                entity.HasOne(d => d.MitarbeiterTest)
                    .WithMany(p => p.BugMitarbeiterTests)
                    .HasForeignKey(d => d.MitarbeiterTestId)
                    .HasConstraintName("FK__Bug__Mitarbeiter__2C3393D0");

                entity.HasOne(d => d.Projekt)
                    .WithMany(p => p.Bugs)
                    .HasForeignKey(d => d.ProjektId)
                    .HasConstraintName("FK__Bug__ProjektID__2B3F6F97");
            });

            modelBuilder.Entity<Mitarbeiter>(entity =>
            {
                entity.ToTable("Mitarbeiter");

                entity.Property(e => e.MitarbeiterId).HasColumnName("MitarbeiterID");

                entity.Property(e => e.Abteilung)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Nachname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Vorname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MitarbeiterProjekt>(entity =>
            {
                entity.ToTable("MitarbeiterProjekt");

                entity.Property(e => e.MitarbeiterProjektId).HasColumnName("MitarbeiterProjektID");

                entity.Property(e => e.MitarbeiterId).HasColumnName("MitarbeiterID");

                entity.Property(e => e.ProjektId).HasColumnName("ProjektID");

                entity.HasOne(d => d.Mitarbeiter)
                    .WithMany(p => p.MitarbeiterProjekts)
                    .HasForeignKey(d => d.MitarbeiterId)
                    .HasConstraintName("FK__Mitarbeit__Mitar__276EDEB3");

                entity.HasOne(d => d.Projekt)
                    .WithMany(p => p.MitarbeiterProjekts)
                    .HasForeignKey(d => d.ProjektId)
                    .HasConstraintName("FK__Mitarbeit__Proje__286302EC");
            });

            modelBuilder.Entity<Projekt>(entity =>
            {
                entity.ToTable("Projekt");

                entity.Property(e => e.ProjektId).HasColumnName("ProjektID");

                entity.Property(e => e.Projektname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

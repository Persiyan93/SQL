using Microsoft.EntityFrameworkCore;
using System;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data
{
    public class HospitalContext:DbContext
    {
        public HospitalContext()
        {

        }

        public HospitalContext(DbContextOptions options)
            :base(options)
        {
          

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=HospitalDatabase;Integrated security=true");
            }
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Diagnose> Diagnoses { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<PatientMedicament> PatientMedicaments { get; set; }

        public DbSet<Visitation> Visitations { get; set; }
    





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visitation>(entity => {
                entity
                    .HasOne(v => v.Patient)
                    .WithMany(p => p.Visitations)
                    .HasForeignKey(v => v.PatientId);

                
            });

            modelBuilder.Entity<Diagnose>(entity => {

                entity
                    .HasOne(d => d.Patient)
                    .WithMany(p => p.Diagnoses)
                    .HasForeignKey(d => d.PatientId);
            
            });


            modelBuilder.Entity<PatientMedicament>(entity =>
            {
                entity.HasKey(x => new { x.PatientId, x.MedicamentId });

                entity
                    .HasOne(x => x.Patient)
                    .WithMany(p => p.Prescriptions)
                    .HasForeignKey(x => x.PatientId);

                entity
                    .HasOne(x => x.Medicament)
                    .WithMany(m => m.Prescriptions)
                    .HasForeignKey(x => x.MedicamentId);

            });


           
           
        }
    }
}

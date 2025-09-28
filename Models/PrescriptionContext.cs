using Microsoft.EntityFrameworkCore;

namespace PrescriptionApp.Models
{
    public class PrescriptionContext : DbContext
    {
        public PrescriptionContext(DbContextOptions<PrescriptionContext> options)
            : base(options) { }

        public DbSet<Prescription> Prescriptions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed a few rows
            modelBuilder.Entity<Prescription>().HasData(
                new Prescription
                {
                    PrescriptionId = 1,
                    MedicationName = "Atorvastatin",
                    FillStatus = "New",
                    Cost = 19.99,
                    RequestTime = new DateTime(2025, 9, 10, 9, 30, 0)
                },
                new Prescription
                {
                    PrescriptionId = 2,
                    MedicationName = "Metformin",
                    FillStatus = "Filled",
                    Cost = 7.49,
                    RequestTime = new DateTime(2025, 9, 12, 14, 10, 0)
                },
                new Prescription
                {
                    PrescriptionId = 3,
                    MedicationName = "Lisinopril",
                    FillStatus = "Pending",
                    Cost = 12.00,
                    RequestTime = new DateTime(2025, 9, 14, 11, 5, 0)
                }
            );
        }
    }
}

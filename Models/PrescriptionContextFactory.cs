using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PrescriptionApp.Models
{
    public class PrescriptionContextFactory : IDesignTimeDbContextFactory<PrescriptionContext>
    {
        public PrescriptionContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PrescriptionContext>();
            optionsBuilder.UseSqlite("Data Source=Prescription.db");

            return new PrescriptionContext(optionsBuilder.Options);
        }
    }
}

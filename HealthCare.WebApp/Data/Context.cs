using HealthCare.Core.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Core.Data
{
    public class Context : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Worker> Workers { get; set; }

        public Context(DbContextOptions<Context> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
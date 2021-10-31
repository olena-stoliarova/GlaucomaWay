using Microsoft.EntityFrameworkCore;

namespace GlaucomaWay.Models
{
    public class GlaucomaDbContext : DbContext
    {
        public GlaucomaDbContext(DbContextOptions<GlaucomaDbContext> options)
            : base(options)
        {
        }

        public GlaucomaDbContext()
        {
        }

        public DbSet<Vf14ResultModel> Vf14Results { get; set; }

        public DbSet<PatientModel> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
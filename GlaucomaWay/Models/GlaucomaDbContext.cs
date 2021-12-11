using GlaucomaWay.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GlaucomaWay.Models
{
    public class GlaucomaDbContext : IdentityDbContext<User, Role, string>
    {
        public GlaucomaDbContext(DbContextOptions<GlaucomaDbContext> options)
            : base(options)
        {
        }

        public GlaucomaDbContext()
        {
        }

        public new DbSet<User> Users => Set<User>();

        public new DbSet<Role> Roles { get; set; }

        public DbSet<Vf14ResultModel> Vf14Results { get; set; }

        public DbSet<PatientModel> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
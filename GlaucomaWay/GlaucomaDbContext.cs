using GlaucomaWay.Models;
using Microsoft.EntityFrameworkCore;

namespace GlaucomaWay
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
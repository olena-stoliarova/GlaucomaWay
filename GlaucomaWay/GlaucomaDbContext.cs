using GlaucomaWay.Models;
using Microsoft.EntityFrameworkCore;

namespace GlaucomaWay
{
    public class GlaucomaDbContext : DbContext
    {
        public DbSet<Vf14ResultModel> Vf14Results { get; set; }

        public GlaucomaDbContext(DbContextOptions<GlaucomaDbContext> options)
         : base(options)
        {
        }

        public GlaucomaDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

    }
}
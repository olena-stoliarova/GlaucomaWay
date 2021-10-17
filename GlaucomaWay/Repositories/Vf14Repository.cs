using GlaucomaWay.Models;

namespace GlaucomaWay.Repositories
{
    public class Vf14Repository : GenericRepository<Vf14ResultModel>, IVf14Repository
    {
        public Vf14Repository(GlaucomaDbContext context)
            : base(context)
        {
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using GlaucomaWay.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace GlaucomaWay.Repositories
{
    public class Vf14Repository : GenericRepository<Vf14ResultModel>, IVf14Repository
    {
        private readonly GlaucomaDbContext _context = null;

        public Vf14Repository(GlaucomaDbContext context)
            : base(context)
        {
            _context = context;
        }

        public async Task<Vf14ResultModel> GetByIdWithPatientAsync(int id, CancellationToken cancellationToken)
            => await _context.Vf14Results
            .Where(i => i.Id == id)
            .Include(i => i.Patient)
            .FirstOrDefaultAsync(cancellationToken);
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GlaucomaWay.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {
        protected GlaucomaDbContext _context = null;
        protected DbSet<TEntity> _table = null;

        public GenericRepository(GlaucomaDbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken)
            => await _table.AsNoTracking().ToListAsync(cancellationToken);

        public async Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken)
            => await _table.FindAsync(new object[] { id }, cancellationToken);

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            var result = await _table.AddAsync(entity, cancellationToken);
            return result.Entity;
        }

        public void Update(TEntity obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            TEntity existing = _table.Find(id);
            _table.Remove(existing);
        }

        public async Task SaveAsync(CancellationToken cancellationToken)
            => await _context.SaveChangesAsync(cancellationToken);
    }
}

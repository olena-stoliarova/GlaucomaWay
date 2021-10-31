using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GlaucomaWay.Models;
using Microsoft.EntityFrameworkCore;

namespace GlaucomaWay.Repositories
{
    public class GenericRepository<TEntity>
        where TEntity : class
    {
        private readonly GlaucomaDbContext _context = null;
        private readonly DbSet<TEntity> _table = null;

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
            await _context.SaveChangesAsync(cancellationToken);
            return result.Entity;
        }

        public async Task UpdateAsync(TEntity obj, CancellationToken cancellationToken)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(object id, CancellationToken cancellationToken)
        {
            TEntity existing = _table.Find(id);
            _table.Remove(existing);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

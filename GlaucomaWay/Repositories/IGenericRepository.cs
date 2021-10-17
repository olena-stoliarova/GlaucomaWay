using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GlaucomaWay.Repositories
{
    public interface IGenericRepository<TEntity>
        where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);

        void Delete(object id);

        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

        Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken);

        Task SaveAsync(CancellationToken cancellationToken);

        void Update(TEntity obj);
    }
}
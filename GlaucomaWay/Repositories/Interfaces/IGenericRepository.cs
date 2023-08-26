using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GlaucomaWay.Repositories.Interfaces;

public interface IGenericRepository<TEntity>
    where TEntity : class
{
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);

    Task DeleteAsync(object id, CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<TEntity> GetByIdAsync(object id, CancellationToken cancellationToken);

    Task UpdateAsync(TEntity obj, CancellationToken cancellationToken);
}
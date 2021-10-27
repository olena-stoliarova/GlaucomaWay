using System.Threading;
using System.Threading.Tasks;
using GlaucomaWay.Models;

namespace GlaucomaWay.Repositories
{
    public interface IVf14Repository : IGenericRepository<Vf14ResultModel>
    {
        Task<Vf14ResultModel> GetByIdWithPatientAsync(int id, CancellationToken cancellationToken);
    }
}

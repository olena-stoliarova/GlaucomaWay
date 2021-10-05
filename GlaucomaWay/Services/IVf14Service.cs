using GlaucomaWay.Models;
using System.Threading;
using System.Threading.Tasks;

namespace GlaucomaWay.Services
{
    public interface IVf14Service
    {
        Task<Vf14ResultModel> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}
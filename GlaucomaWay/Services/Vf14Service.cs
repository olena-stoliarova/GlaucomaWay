using GlaucomaWay.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GlaucomaWay.Services
{
    public class Vf14Service : IVf14Service
    {
        private readonly GlaucomaDbContext _glaucomaDbContext;
        public Vf14Service(GlaucomaDbContext glaucomaDbContext)
        {
            _glaucomaDbContext = glaucomaDbContext;
        }

        public async Task<Vf14ResultModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _glaucomaDbContext.Vf14Results.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        }
    }
}

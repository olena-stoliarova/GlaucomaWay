using GlaucomaWay.Models;

namespace GlaucomaWay.Repositories
{
    public class PatientRepository : GenericRepository<PatientModel>, IPatientRepository
    {
        public PatientRepository(GlaucomaDbContext context)
            : base(context)
        {
        }
    }
}

using System.Threading;
using System.Threading.Tasks;
using GlaucomaWay.Models;
using GlaucomaWay.Repositories.Interfaces;

namespace GlaucomaWay.Repositories;

public class PatientRepository : GenericRepository<PatientModel>, IPatientRepository
{
    private readonly GlaucomaDbContext _context;

    public PatientRepository(GlaucomaDbContext context)
        : base(context)
    {
        _context = context;
    }

    public override async Task<PatientModel> CreateAsync(PatientModel entity, CancellationToken cancellationToken)
    {
        var patient = await base.CreateAsync(entity, cancellationToken);

        var user = await _context.Users.FindAsync(entity.UserId);
        user.PatientId = patient.Id.ToString();

        await _context.SaveChangesAsync();

        return patient;
    }

}
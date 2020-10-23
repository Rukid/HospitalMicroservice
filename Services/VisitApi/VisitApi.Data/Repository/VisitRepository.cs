using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VisitApi.Data.Database;
using VisitApi.Domain;

namespace VisitApi.Data.Repository
{
    public class VisitRepository : Repository<Visit>, IVisitRepository
    {
        public VisitRepository(VisitContext orderContext) : base(orderContext)
        {
        }

        public async Task<List<Visit>> GetInitialVisitsAsync(CancellationToken cancellationToken)
        {
            return await VisitContext.Visits.Where(x => x.VisitId == 1).ToListAsync(cancellationToken);
        }

        public async Task<List<Visit>> GetRepeatedVisitsAsync(CancellationToken cancellationToken)
        {
            return await VisitContext.Visits.Where(x => x.VisitId == 2).ToListAsync(cancellationToken);
        }

        public async Task<Visit> GetVisitByIdAsync(Guid visitId, CancellationToken cancellationToken)
        {
            return await VisitContext.Visits.FirstOrDefaultAsync(x => x.Id == visitId, cancellationToken);
        }

        public async Task<List<Visit>> GetVisitByDoctorIdGuidAsync(Guid doctorId, CancellationToken cancellationToken)
        {
            return await VisitContext.Visits.Where(x => x.DoctorGuid == doctorId).ToListAsync(cancellationToken);
        }

        public async Task<List<Visit>> GetVisitByClientIdGuidAsync(Guid clientId, CancellationToken cancellationToken)
        {
            return await VisitContext.Visits.Where(x => x.ClientGuid == clientId).ToListAsync(cancellationToken);
        }
    }
}
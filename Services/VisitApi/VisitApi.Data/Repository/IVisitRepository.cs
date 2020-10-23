using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using VisitApi.Domain;

namespace VisitApi.Data.Repository
{
    public interface IVisitRepository: IRepository<Visit>
    {
        Task<List<Visit>> GetInitialVisitsAsync(CancellationToken cancellationToken);
        Task<List<Visit>> GetRepeatedVisitsAsync(CancellationToken cancellationToken);
        Task<Visit> GetVisitByIdAsync(Guid visitId, CancellationToken cancellationToken);
        Task<List<Visit>> GetVisitByDoctorIdGuidAsync(Guid doctorId, CancellationToken cancellationToken);
        Task<List<Visit>> GetVisitByClientIdGuidAsync(Guid clientId, CancellationToken cancellationToken);
    }
}
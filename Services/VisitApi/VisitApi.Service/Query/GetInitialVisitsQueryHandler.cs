using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VisitApi.Data.Repository;
using VisitApi.Domain;

namespace VisitApi.Service.Query
{
    public class GetInitialVisitsQueryHandler : IRequestHandler<GetInitialVisitsQuery, List<Visit>>
    {
        private readonly IVisitRepository _visitRepository;

        public GetInitialVisitsQueryHandler(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<List<Visit>> Handle(GetInitialVisitsQuery request, CancellationToken cancellationToken)
        {
            return await _visitRepository.GetInitialVisitsAsync(cancellationToken);
        }
    }
}
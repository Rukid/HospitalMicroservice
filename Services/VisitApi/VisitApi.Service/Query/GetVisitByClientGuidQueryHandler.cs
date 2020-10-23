using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VisitApi.Data.Repository;
using VisitApi.Domain;

namespace VisitApi.Service.Query
{
    public class GetVisitByClientGuidQueryHandler : IRequestHandler<GetVisitByClientGuidQuery, List<Visit>>
    {
        private readonly IVisitRepository _visitRepository;

        public GetVisitByClientGuidQueryHandler(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<List<Visit>> Handle(GetVisitByClientGuidQuery request, CancellationToken cancellationToken)
        {
            return await _visitRepository.GetVisitByClientIdGuidAsync(request.ClientId, cancellationToken);
        }
    }
}
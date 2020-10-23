using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VisitApi.Data.Repository;
using VisitApi.Domain;

namespace VisitApi.Service.Query
{
    public class GetVisitByIdQueryHandler : IRequestHandler<GetVisitByIdQuery, Visit>
    {
        private readonly IVisitRepository _visitRepository;

        public GetVisitByIdQueryHandler(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<Visit> Handle(GetVisitByIdQuery request, CancellationToken cancellationToken)
        {
            return await _visitRepository.GetVisitByIdAsync(request.Id, cancellationToken);
        }
    }
}
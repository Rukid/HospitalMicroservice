using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VisitApi.Data.Repository;
using VisitApi.Domain;

namespace VisitApi.Service.Command
{
    public class VisitCompletedCommandHandler : IRequestHandler<VisitCompletedCommand, Visit>
    {
        private readonly IVisitRepository _visitRepository;

        public VisitCompletedCommandHandler(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<Visit> Handle(VisitCompletedCommand request, CancellationToken cancellationToken)
        {
            return await _visitRepository.UpdateAsync(request.Visit);
        }
    }
}
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VisitApi.Data.Repository;

namespace VisitApi.Service.Command
{
    public class UpdateVisitCommandHandler : IRequestHandler<UpdateVisitCommand>
    {
        private readonly IVisitRepository _visitRepository;

        public UpdateVisitCommandHandler(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<Unit> Handle(UpdateVisitCommand request, CancellationToken cancellationToken)
        {
            await _visitRepository.UpdateRangeAsync(request.Visits);

            return Unit.Value;
        }
    }
}
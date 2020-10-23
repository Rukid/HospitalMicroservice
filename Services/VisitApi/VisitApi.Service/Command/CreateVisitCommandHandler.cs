using System.Threading;
using System.Threading.Tasks;
using MediatR;
using VisitApi.Data.Repository;
using VisitApi.Domain;

namespace VisitApi.Service.Command
{
    public class CreateVisitCommandHandler : IRequestHandler<CreateVisitCommand, Visit>
    {
        private readonly IVisitRepository _visitRepository;

        public CreateVisitCommandHandler(IVisitRepository visitRepository)
        {
            _visitRepository = visitRepository;
        }

        public async Task<Visit> Handle(CreateVisitCommand request, CancellationToken cancellationToken)
        {
            return await _visitRepository.AddAsync(request.Visit);
        }
    }
}
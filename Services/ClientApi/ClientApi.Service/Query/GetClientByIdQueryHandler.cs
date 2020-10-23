using ClientApi.Data.Repository;
using ClientApi.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApi.Service.Query
{
    public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, Client>
    {
        private readonly IClientRepository _clientRepository;

        public GetClientByIdQueryHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
        {
            return await _clientRepository.GetClientByIdAsync(request.Id, cancellationToken);
        }
    }
}

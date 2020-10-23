using ClientApi.Data.Repository;
using ClientApi.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApi.Service.Command
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, Client>
    {
        private readonly IClientRepository _clientRepository;

        public CreateClientCommandHandler(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            return await _clientRepository.AddAsync(request.Client);
        }
    }
}

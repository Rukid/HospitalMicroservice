using ClientApi.Data.Repository;
using ClientApi.Domain.Entities;
using ClientApi.Messaging.Send.Sender;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClientApi.Service.Command
{
    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, Client>
    {
        private readonly IClientRepository _clientRepository;   
        private readonly IClientUpdateSender _clientUpdateSender;

        public UpdateClientCommandHandler(IClientRepository clientRepository, IClientUpdateSender clientUpdateSender)
        {
            _clientRepository = clientRepository;
            _clientUpdateSender = clientUpdateSender;
        }

        public async Task<Client> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var client = await _clientRepository.UpdateAsync(request.Client);

            _clientUpdateSender.SendClient(client);

            return client;
        }
    }
}

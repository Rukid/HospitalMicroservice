using ClientApi.Domain.Entities;
using MediatR;

namespace ClientApi.Service.Command
{
    public class UpdateClientCommand : IRequest<Client>
    {
        public Client Client { get; set; }
    }
}

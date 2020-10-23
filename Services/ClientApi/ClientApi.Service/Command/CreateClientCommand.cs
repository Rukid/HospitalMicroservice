using ClientApi.Domain.Entities;
using MediatR;

namespace ClientApi.Service.Command
{
    public class CreateClientCommand : IRequest<Client>
    {
        public Client Client { get; set; }
    }
}

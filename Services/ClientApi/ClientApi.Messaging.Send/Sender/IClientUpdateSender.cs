using ClientApi.Domain.Entities;

namespace ClientApi.Messaging.Send.Sender
{
    public interface IClientUpdateSender
    {
        void SendClient(Client client);
    }
}

using ClientApi.Domain.Entities;
using ClientApi.Messaging.Send.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace ClientApi.Messaging.Send.Sender
{
    public class ClientUpdateSender : IClientUpdateSender
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _username;
        private readonly string _password;

        public ClientUpdateSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _hostname = rabbitMqOptions.Value.Hostname;
            _queueName = rabbitMqOptions.Value.QueueName;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
        }

        public void SendClient(Client client)
        {
            var factory = new ConnectionFactory() { HostName = _hostname, UserName = _username, Password = _password };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var json = JsonConvert.SerializeObject(client);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
        }
    }
}

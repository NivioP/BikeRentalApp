using BikeRentalApp.Application.Interfaces;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BikeRentalApp.Infrastructure.Messaging {
    public class RabbitMqEventPublisher : IEventPublisher {
        private readonly RabbitMqConnection _connection;

        public RabbitMqEventPublisher(RabbitMqConnection connection) {
            _connection = connection;
        }

        public async Task PublishAsync<T>(string topic, T message) {
            var jsonMessage = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(jsonMessage);

            _connection.Channel.QueueDeclare(queue: topic, durable: true, exclusive: false, autoDelete: false, arguments: null);

            _connection.Channel.BasicPublish(
                exchange: "",
                routingKey: topic,
                basicProperties: null,
                body: body
            );

            await Task.CompletedTask; 
        }
    }
}

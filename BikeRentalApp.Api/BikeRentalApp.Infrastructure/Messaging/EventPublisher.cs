using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace BikeRentalApp.Infrastructure.Messaging {
    public class EventPublisher {
        private readonly RabbitMqConnection _rabbitMqConnection;

        public EventPublisher(RabbitMqConnection rabbitMqConnection) {
            _rabbitMqConnection = rabbitMqConnection;
        }

        public void Publish<T>(string queueName, T message) {
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            _rabbitMqConnection.Channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _rabbitMqConnection.Channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
        }
    }
}

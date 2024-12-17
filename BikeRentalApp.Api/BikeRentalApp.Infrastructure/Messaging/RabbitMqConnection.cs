using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;

namespace BikeRentalApp.Infrastructure.Messaging {
    public class RabbitMqConnection : IDisposable {
        private readonly IConnection _connection;
        public IModel Channel { get; }

        public RabbitMqConnection(IConfiguration config) {
            var factory = new ConnectionFactory() {
                HostName = config["RabbitMQ:HostName"] ?? "localhost",
                Port = int.Parse(config["RabbitMQ:Port"] ?? "5672"), 
                UserName = config["RabbitMQ:UserName"] ?? "admin",
                Password = config["RabbitMQ:Password"] ?? "admin"
            };

            _connection = factory.CreateConnection();
            Channel = _connection.CreateModel();
        }

        public void Dispose() {
            Channel.Close();
            _connection.Close();
        }
    }
}

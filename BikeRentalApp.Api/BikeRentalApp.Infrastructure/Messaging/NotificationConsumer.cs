using BikeRentalApp.Application.DTOs;
using BikeRentalApp.Domain.Events;
using BikeRentalApp.Domain.Interfaces;
using BikeRentalApp.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace BikeRentalApp.Infrastructure.Messaging {
    public class NotificationConsumer : BackgroundService {
        private readonly RabbitMqConnection _connection;
        private readonly IServiceScopeFactory _scopeFactory;
        private IModel _channel;

        public NotificationConsumer(RabbitMqConnection connection, IServiceScopeFactory scopeFactory) {
            _connection = connection;
            _scopeFactory = scopeFactory;
            _channel = _connection.Channel;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken) {
            _channel.QueueDeclare(queue: "moto.created", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                try {
                    var motoEvent = JsonSerializer.Deserialize<MotoCreatedEvent>(message);

                    if (motoEvent.Ano == 2024) {
                        using var scope = _scopeFactory.CreateScope();
                        var mongoRepository = scope.ServiceProvider.GetRequiredService<IMongoNotificationRepository>();

                        var notificacao = new MotoCreatedNotification {
                            Identificador = motoEvent.Identificador,
                            Ano = motoEvent.Ano,
                            Modelo = motoEvent.Modelo,
                            Placa = motoEvent.Placa,
                            DataNotificacao = DateTime.UtcNow
                        };

                        await mongoRepository.AddAsync(notificacao);
                    }

                    _channel.BasicAck(e.DeliveryTag, false);
                }
                catch (Exception ex) {
                    Console.WriteLine($"Erro while processing the messsage: {ex.Message}");
                }
            };

            _channel.BasicConsume(queue: "moto.created", autoAck: false, consumer: consumer);

            return Task.CompletedTask;
        }
        public override void Dispose() {
            _channel?.Close();
            base.Dispose();
        }
    }
}

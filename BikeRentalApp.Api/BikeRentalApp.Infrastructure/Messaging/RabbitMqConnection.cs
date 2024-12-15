using RabbitMQ.Client;

namespace BikeRentalApp.Infrastructure.Messaging {
    public class RabbitMqConnection : IDisposable{
        private readonly IConnection _connection;
        public IModel Channel { get; }

        public RabbitMqConnection() {
            var factory = new ConnectionFactory() {
                HostName = "localhost", 
                Port = 5672,            
                UserName = "guest",     
                Password = "guest"      
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

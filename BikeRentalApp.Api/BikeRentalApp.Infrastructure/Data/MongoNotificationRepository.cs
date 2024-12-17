using BikeRentalApp.Domain.Events;
using BikeRentalApp.Domain.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BikeRentalApp.Infrastructure.Data {
    public class MongoNotificationRepository : IMongoNotificationRepository {
        private readonly IMongoCollection<MotoCreatedNotification> _collection;

        public MongoNotificationRepository(IOptions<MongoSettings> settings) {
            var connectionString = settings.Value.ConnectionString ?? "mongodb://localhost:27017/";
            var client = new MongoClient(connectionString); 
            var database = client.GetDatabase(settings.Value.DatabaseName);
            _collection = database.GetCollection<MotoCreatedNotification>("Notificacoes");
        }

        public async Task AddAsync(MotoCreatedNotification notificacao) {
            await _collection.InsertOneAsync(notificacao);
        }
    }
}

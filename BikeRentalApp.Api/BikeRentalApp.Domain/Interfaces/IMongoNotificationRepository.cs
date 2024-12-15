using BikeRentalApp.Domain.Events;

namespace BikeRentalApp.Domain.Interfaces {
    public interface IMongoNotificationRepository {
        Task AddAsync(MotoCreatedNotification notification);
    }
}


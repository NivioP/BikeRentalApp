
using BikeRentalApp.Application.Interfaces;
using BikeRentalApp.Application.Services;
using BikeRentalApp.Domain.Interfaces;
using BikeRentalApp.Infrastructure.Data;
using BikeRentalApp.Infrastructure.Messaging;
using BikeRentalApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalAPI {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // Configure Swagger/OpenAPI
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure PostgreSQL
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

            // Register repository and service
            builder.Services.AddScoped<IMotoRepository, MotoRepository>();
            builder.Services.AddScoped<IMotoService, MotoService>();

            // Configure messaging system
            builder.Services.AddSingleton<RabbitMqConnection>();
            builder.Services.AddSingleton<EventPublisher>();
            builder.Services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();

            // Configure MongoDB settings
            builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
            builder.Services.AddScoped<IMongoNotificationRepository, MongoNotificationRepository>();

            // Add hosted service for notification consumer
            builder.Services.AddHostedService<NotificationConsumer>();

            // Configure Npgsql for EF Core
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment()) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

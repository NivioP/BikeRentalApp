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
            builder.Services.AddScoped<IEntregadorRepository, EntregadorRepository>();
            builder.Services.AddScoped<IEntregadorService, EntregadorService>();
            builder.Services.AddScoped<ILocacaoRepository, LocacaoRepository>();
            builder.Services.AddScoped<ILocacaoService, LocacaoService>();

            // Configure messaging system
            builder.Services.AddSingleton<RabbitMqConnection>(sp => {
                var config = sp.GetRequiredService<IConfiguration>();
                return new RabbitMqConnection(config);
            });
            builder.Services.AddSingleton<EventPublisher>();
            builder.Services.AddSingleton<IEventPublisher, RabbitMqEventPublisher>();

            // Configure MongoDB settings
            builder.Services.Configure<MongoSettings>(builder.Configuration.GetSection("MongoSettings"));
            builder.Services.AddScoped<IMongoNotificationRepository, MongoNotificationRepository>();

            // Configure Amazon S3 settings
            builder.Services.AddScoped<IS3Service, S3Service>(sp => {
                var config = sp.GetRequiredService<IConfiguration>();
                return new S3Service(config);
            });

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

            using (var scope = app.Services.CreateScope()) {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate(); // Aplica as migrations automaticamente
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

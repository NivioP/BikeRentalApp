
using BikeRentalApp.Application.Interfaces;
using BikeRentalApp.Application.Services;
using BikeRentalApp.Domain.Interfaces;
using BikeRentalApp.Infrastructure.Data;
using BikeRentalApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BikeRentalAPI {
    public class Program {
        public static void Main(string[] args) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IMotoRepository, MotoRepository>();
            builder.Services.AddScoped<IMotoService, MotoService>();

            builder.Services.AddScoped<MotoService>();
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

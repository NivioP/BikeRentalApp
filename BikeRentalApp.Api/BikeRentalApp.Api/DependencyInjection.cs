using BikeRentalAPI.Application;
using BikeRentalAPI.Infrastructure;

namespace BikeRentalAPI {
    public static class DependencyInjection {

        public static IServiceCollection AddAppDI(this IServiceCollection services) {
            services.AddApplicationDI()
                .AddInfrastructureDI();
            return services;
        }
    }
}

﻿using Microsoft.Extensions.DependencyInjection;

namespace BikeRentalAPI.Infrastructure {
    public static class DependencyInjection {
        public static IServiceCollection AddInfrastructureDI(this IServiceCollection services) {
            return services;
        }
    }
}

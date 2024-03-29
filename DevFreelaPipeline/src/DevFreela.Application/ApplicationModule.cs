using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Consumers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevFreela.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services) {
            services
                .AddMediatR(typeof(CreateProjectCommand));

            return services;
        }

        private static IServiceCollection AddConsumers(this IServiceCollection services) {
            services.AddHostedService<PaymentApprovedConsumer>();
            
            return services;
        }
    }
}
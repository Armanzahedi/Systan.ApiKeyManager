using Microsoft.Extensions.DependencyInjection;
using Systan.ApiKeyManager.Core.Interfaces;
using Systan.ApiKeyManager.DataAccess.Repositories;
using Systan.ApiKeyManager.Service.Factories;
using Systan.ApiKeyManager.Service.Manager;
using Systan.ApiKeyManager.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Service
{
    public static class Startup
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddScoped<IGatewayService, GatewayService>();
            services.AddScoped<IMessageBusManager, SystanMessageBusManager>();
            services.AddScoped<IBusMessageResolverFactory, BusMessageResolverFactory>();

            services.AddAutoMapper(typeof(Startup));

            return services;
        }
    }
}

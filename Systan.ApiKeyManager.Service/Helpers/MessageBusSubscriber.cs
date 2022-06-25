using Microsoft.Extensions.Configuration;
using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
using Systan.ApiKeyManager.Core.Interfaces;
using Systan.ApiKeyManager.Service.Manager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Service.Helpers
{
    public static class MessageBusSubscriber
    {
        public static void SubscribeToEvents(IConfiguration configuration)
        {
            var busMan = new SystanMessageBusManager();

            var address = configuration["SystanMessageBus:Address"];
            var services = configuration.GetSection("SystanMessageBus:Services").GetChildren().Select(a => a.Value).ToList();
            var subjects = configuration.GetSection("SystanMessageBus:Subjects").GetChildren().Select(a => a.Value).ToList();

            if(services != null && services.Any() && subjects != null && subjects.Any())
            {
                foreach(var service in services)
                {
                    foreach (var subject in subjects)
                    {
                        busMan.Subscribe(address, new SubscribeRequest
                        {
                            Endpoint = address,
                            Service = service,
                            Subject = subject
                        });
                    }
                }
            }
        }
    }
}

using Microsoft.Extensions.Configuration;
using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
using Systan.ApiKeyManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Service.Manager
{
    public class SystanMessageBusManager : IMessageBusManager
    {
        private readonly IBusMessageResolver? _resolver;

        public SystanMessageBusManager()
        {
        }

        public SystanMessageBusManager(IBusMessageResolver? resolver)
        {
            _resolver = resolver;
        }
     
        public Task Subscribe(string subscribeUrl, SubscribeRequest request)
        {
            throw new NotImplementedException();
        }
        public async Task OnMessage(BaseBusMessage message)
        {
            if (_resolver == null)
                throw new Exception("Please provide a MessageResolver. SystanBusManager doesn't have a MessageResolver.");

            await _resolver.ResolveMessage(message);
        }
    }
}

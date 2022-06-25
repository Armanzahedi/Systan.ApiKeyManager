using Microsoft.AspNetCore.Mvc;
using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
using Systan.ApiKeyManager.Core.Interfaces;
using Systan.ApiKeyManager.Service.Manager;

namespace Systan.ApiKeyManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListenerController : ControllerBase
    {
        private readonly IBusMessageResolverFactory _busMessageResolverFactory;

        public ListenerController(IBusMessageResolverFactory busMessageResolverFactory)
        {
            _busMessageResolverFactory = busMessageResolverFactory;
        }

        [HttpPost]
        public async Task Post([FromBody] BaseBusMessage message)
        {
            var messageResolver = _busMessageResolverFactory.CreateInstance(message);

            var busMan = new SystanMessageBusManager(messageResolver);
            await busMan.OnMessage(message);
        }
    }
}

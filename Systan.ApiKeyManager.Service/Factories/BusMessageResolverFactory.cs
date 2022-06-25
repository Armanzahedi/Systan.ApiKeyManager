using AutoMapper;
using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
using Systan.ApiKeyManager.Core.Helpers;
using Systan.ApiKeyManager.Core.Interfaces;
using Systan.ApiKeyManager.Core.Util;
using Systan.ApiKeyManager.Service.Resolvers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Service.Factories
{
    public class BusMessageResolverFactory : IBusMessageResolverFactory
    {
        private readonly IApiKeyRepository _apiKeyRepo;
        private readonly IGatewayService _gatewayService;
        private readonly IMapper _mapper;

        public BusMessageResolverFactory(IGatewayService gatewayService,
            IApiKeyRepository apiKeyRepo,
            IMapper mapper)
        {
            _gatewayService = gatewayService;
            _apiKeyRepo = apiKeyRepo;
            _mapper = mapper;
        }

        public IBusMessageResolver CreateInstance(BaseBusMessage message)
        {
            if (string.IsNullOrWhiteSpace(message.Subject))
                throw new ArgumentException("Subject", "Message Subject is undefined");

            var resolverType = Assembly.GetExecutingAssembly().GetTypes()
                .FirstOrDefault(t => t.IsAssignableTo(typeof(IBusMessageResolver)) &&
                t.IsDefined(typeof(BusMessageAttribute)) && 
                t.GetCustomAttribute<BusMessageAttribute>()?._busMessageName == message.Subject);

            if(resolverType == null)
                throw new ArgumentException("Subject", "Could not find Resolver Message Subject");

            return (IBusMessageResolver)Activator.CreateInstance(resolverType, _apiKeyRepo, _gatewayService, _mapper)!;
        }
    }
}

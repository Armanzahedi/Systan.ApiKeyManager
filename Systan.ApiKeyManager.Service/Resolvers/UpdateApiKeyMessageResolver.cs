using AutoMapper;
using Systan.ApiKeyManager.Core.Dtos.GatewayDtos;
using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
using Systan.ApiKeyManager.Core.Entities;
using Systan.ApiKeyManager.Core.Helpers;
using Systan.ApiKeyManager.Core.Interfaces;
using Systan.ApiKeyManager.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Systan.ApiKeyManager.Service.Resolvers
{
    [BusMessage(BusMessages.UPDATE_APIKEY)]
    public class UpdateApiKeyMessageResolver : IBusMessageResolver
    {
        private readonly IApiKeyService _apiKeyService;
        private readonly IGatewayService _gatewayService;
        private readonly IMapper _mapper;

        public UpdateApiKeyMessageResolver(IApiKeyService apiKeyService,
            IGatewayService gatewayService,
            IMapper mapper)
        {
            _apiKeyService = apiKeyService;
            _gatewayService = gatewayService;
            _mapper = mapper;
        }

        public async Task ResolveMessage(BaseBusMessage baseMessage)
        {
            try
            {
                UpdateApiKeyMessage message = _mapper.Map<UpdateApiKeyMessage>(baseMessage);

                if (message.Body == null || message.Body.ApiKeyId == null || message.Body.NewApiKey == null)
                    throw new Exception("Invalid Message Body.");

                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {

                    var updateModel = new ApiKey
                    {
                        Key = message.Body.NewApiKey,
                        ServiceId = message.ServiceId,
                        SystanId = message.Body.ApiKeyId
                    };

                    await _apiKeyService.UpdateApiKey(updateModel);
                    await _gatewayService.UpdateApiKey(message.Body.ApiKeyId, new UpdateApiKeyRequest { key = message.Body.NewApiKey});

                    ts.Complete();
                }


            }
            catch (Exception e)
            {
                throw new Exception($"Could not resolve Message: {e.Message}", e);
            }
        }
    }
}

using AutoMapper;
using Systan.ApiKeyManager.Core.Dtos.GatewayDtos;
using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
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
        private readonly IApiKeyRepository _apiKeyRepo;
        private readonly IGatewayService _gatewayService;
        private readonly IMapper _mapper;

        public UpdateApiKeyMessageResolver(IApiKeyRepository apiKeyRepo, IGatewayService gatewayService, IMapper mapper)
        {
            _apiKeyRepo = apiKeyRepo;
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
                    var model = await _apiKeyRepo.GetBySystanId(message.Body.ApiKeyId);
                    if(model == null)
                        throw new Exception("ApiKey was not found.");

                    model.Key = message.Body.NewApiKey;
                    model.ServiceId = message.ServiceId;
                    await _apiKeyRepo.Update(model);

                    await _gatewayService.UpdateApiKey(message.Body.ApiKeyId,new UpdateApiKeyRequest { key = message.Body.NewApiKey});

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

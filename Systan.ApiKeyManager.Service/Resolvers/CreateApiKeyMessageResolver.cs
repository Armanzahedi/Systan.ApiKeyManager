using AutoMapper;
using Newtonsoft.Json;
using Systan.ApiKeyManager.Core.Dtos.GatewayDtos;
using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
using Systan.ApiKeyManager.Core.Enums;
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
    [BusMessage(BusMessages.CREATE_APIKEY)]
    public class CreateApiKeyMessageResolver : IBusMessageResolver
    {
        private readonly IApiKeyRepository _apiKeyRepo;
        private readonly IGatewayService _gatewayService;
        private readonly IMapper _mapper;

        public CreateApiKeyMessageResolver(IApiKeyRepository apiKeyRepo, IGatewayService gatewayService, IMapper mapper)
        {
            _apiKeyRepo = apiKeyRepo;
            _gatewayService = gatewayService;
            _mapper = mapper;
        }

        public async Task ResolveMessage(BaseBusMessage baseMessage)
        {
            try
            {
                CreateApiKeyMessage message = _mapper.Map<CreateApiKeyMessage>(baseMessage);

                if (message.Body == null || message.Body.ApiKey == null || message.Body.ApiKeyId == null)
                    throw new Exception("Invalid Message Body.");

                if (_apiKeyRepo.ApiKeyExists(message.Body.ApiKeyId))
                    throw new Exception("ApiKey Already Exists.");

                using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    await _apiKeyRepo.Add(new Core.Entities.ApiKey
                    {
                        ServiceId = message.ServiceId,
                        Key = message.Body.ApiKey,
                        SystanId = message.Body.ApiKeyId,
                        StatusReason = (int)ApiKeyStatusReasons.Active,
                        StatusReasonTitle = "Active"
                    });

                    await _gatewayService.AddApiKey(new AddApiKeyRequest
                    {
                        id = message.Body.ApiKeyId,
                        key = message.Body.ApiKey,
                    });

                    ts.Complete();
                }

             
            }
            catch (Exception e)
            {
                throw new Exception($"Could not resolve Message: {e.Message}",e);
            }
        }
    }
}

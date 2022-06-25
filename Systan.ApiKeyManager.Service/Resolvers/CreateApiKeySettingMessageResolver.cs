using AutoMapper;
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
    [BusMessage(BusMessages.CREATE_APIKEYSETTING)]
    public class CreateApiKeySettingMessageResolver : IBusMessageResolver
    {
        private readonly IApiKeyRepository _apiKeyRepo;
        private readonly IGatewayService _gatewayService;
        private readonly IMapper _mapper;

        public CreateApiKeySettingMessageResolver(IApiKeyRepository apiKeyRepo,
            IGatewayService gatewayService,
            IMapper mapper)
        {
            _apiKeyRepo = apiKeyRepo;
            _gatewayService = gatewayService;
            _mapper = mapper;
        }

        public async Task ResolveMessage(BaseBusMessage baseMessage)
        {
            try
            {
                CreateApiKeySettingMessage message = _mapper.Map<CreateApiKeySettingMessage>(baseMessage);

                if (message.Body == null || message.Body.ApiKeyId == null || message.Body.ApiKeySettingId == null || message.Body.Key == null || message.Body.Value == null)
                    throw new Exception("Invalid Message Body.");

                var model = await _apiKeyRepo.GetSettingBySystanId(message.Body.ApiKeyId);
                if (model != null)
                    throw new Exception("ApiKey Setting Already Exists.");


                var apiKey = _apiKeyRepo.GetBySystanId(message.Body.ApiKeyId);
                if (apiKey == null)
                    throw new Exception("Related ApiKey was not found.");

                await _apiKeyRepo.CreateSetting(new ApiKeySetting
                {
                    ApiKeyId = apiKey.Id,
                    SystanId = message.Body.ApiKeySettingId,
                    Key = message.Body.Key,
                    Value = message.Body.Value
                });
            }
            catch (Exception e)
            {
                throw new Exception($"Could not resolve Message: {e.Message}", e);
            }
        }

    }
}

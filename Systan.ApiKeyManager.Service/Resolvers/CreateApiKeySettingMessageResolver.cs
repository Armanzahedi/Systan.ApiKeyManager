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
        private readonly IApiKeyService _apiKeyService;
        private readonly IGatewayService _gatewayService;
        private readonly IMapper _mapper;

        public CreateApiKeySettingMessageResolver(IApiKeyService apiKeyService,
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
                CreateApiKeySettingMessage message = _mapper.Map<CreateApiKeySettingMessage>(baseMessage);

                if (message.Body == null || message.Body.ApiKeyId == null || message.Body.ApiKeySettingId == null || message.Body.Key == null || message.Body.Value == null)
                    throw new Exception("Invalid Message Body.");

                var setting = new ApiKeySetting
                {
                    SystanId = message.Body.ApiKeySettingId,
                    Key = message.Body.Key,
                    Value = message.Body.Value
                };
                await _apiKeyService.CreateSetting(message.Body.ApiKeyId,setting);

            }
            catch (Exception e)
            {
                throw new Exception($"Could not resolve Message: {e.Message}", e);
            }
        }

    }
}

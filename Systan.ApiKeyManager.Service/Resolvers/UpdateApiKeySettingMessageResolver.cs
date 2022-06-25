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

namespace Systan.ApiKeyManager.Service.Resolvers
{
    [BusMessage(BusMessages.UPDATE_APIKEYSETTING)]
    public class UpdateApiKeySettingMessageResolver : IBusMessageResolver
    {
        private readonly IApiKeyRepository _apiKeyRepo;
        private readonly IGatewayService _gatewayService;
        private readonly IMapper _mapper;

        public UpdateApiKeySettingMessageResolver(IApiKeyRepository apiKeyRepo,
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
                UpdateApiKeySettingMessage message = _mapper.Map<UpdateApiKeySettingMessage>(baseMessage);

                if (message.Body == null || message.Body.ApiKeySettingId == null || message.Body.Key == null || message.Body.Value == null)
                    throw new Exception("Invalid Message Body.");

                var model = await _apiKeyRepo.GetSettingBySystanId(message.Body.ApiKeySettingId);

                if (model == null)
                    throw new Exception("ApiKey Setting was not found.");

                model.Key = message.Body.Key;
                model.Value = message.Body.Value;
                await _apiKeyRepo.UpdateSetting(model);
            }
            catch (Exception e)
            {
                throw new Exception($"Could not resolve Message: {e.Message}", e);
            }
        }

    }
}

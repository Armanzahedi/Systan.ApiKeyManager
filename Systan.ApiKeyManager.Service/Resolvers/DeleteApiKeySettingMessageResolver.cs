using AutoMapper;
using Systan.ApiKeyManager.Core.Dtos.MessageBusDtos;
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
    [BusMessage(BusMessages.DELETE_APIKEYSETTING)]
    public class DeleteApiKeySettingMessageResolver : IBusMessageResolver
    {
        private readonly IApiKeyRepository _apiKeyRepo;
        private readonly IGatewayService _gatewayService;
        private readonly IMapper _mapper;

        public DeleteApiKeySettingMessageResolver(IApiKeyRepository apiKeyRepo,
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
                DeleteApiKeySettingMessage message = _mapper.Map<DeleteApiKeySettingMessage>(baseMessage);

                if (message.Body == null || message.Body.ApiKeySettingId == null || message.Body.Key == null)
                    throw new Exception("Invalid Message Body.");

                await _apiKeyRepo.DeleteSettingBySystanId(message.Body.ApiKeySettingId);
            }
            catch (Exception e)
            {
                throw new Exception($"Could not resolve Message: {e.Message}", e);
            }
        }

    }
}

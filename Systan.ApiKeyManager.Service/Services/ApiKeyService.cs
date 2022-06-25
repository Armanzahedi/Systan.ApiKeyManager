using Systan.ApiKeyManager.Core.Entities;
using Systan.ApiKeyManager.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Service.Services
{
    public class ApiKeyService : IApiKeyService
    {
        private readonly IApiKeyRepository _apiKeyRepo;
        private readonly IGatewayService _gatewayService;

        public ApiKeyService(IApiKeyRepository apiKeyRepo,
            IGatewayService gatewayService)
        {
            _apiKeyRepo = apiKeyRepo;
            _gatewayService = gatewayService;
        }

        public async Task<ApiKey> AddApiKey(ApiKey model)
        {
            if (_apiKeyRepo.ApiKeyExists(model.SystanId))
                throw new Exception("ApiKey Exists");

            await _apiKeyRepo.Add(model);
            await _gatewayService.AddApiKey(new Core.Dtos.GatewayDtos.AddApiKeyRequest { id = model.SystanId, key = model.Key });

            return model;
        }

        public async Task DeleteApiKey(string systanId)
        {
            await _apiKeyRepo.DeleteBySystanId(systanId);
            await _gatewayService.DeleteApiKey(systanId);
        }

        public async Task<ApiKey> UpdateApiKey(ApiKey model)
        {
            await _apiKeyRepo.Update(model);
            await _gatewayService.UpdateApiKey(model.SystanId,new Core.Dtos.GatewayDtos.UpdateApiKeyRequest { key = model.Key });

            return model;
        }
    }
}

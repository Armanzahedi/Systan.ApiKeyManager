using Systan.ApiKeyManager.Core.Entities;
using Systan.ApiKeyManager.Core.Enums;
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

        public ApiKeyService(IApiKeyRepository apiKeyRepo)
        {
            _apiKeyRepo = apiKeyRepo;
        }

        public async Task<ApiKey> AddApiKey(ApiKey model)
        {
            if (_apiKeyRepo.ApiKeyExists(model.SystanId))
                throw new Exception("ApiKey already Exists.");

            model.StatusReason = (int)ApiKeyStatusReasons.Active;
            model.StatusReasonTitle = "Active";
            await _apiKeyRepo.Add(model);

            return model;
        }

        public async Task<ApiKey?> ChangeApiKeyStatusReason(string systanId, int statusReason, string statusReasonTitle)
        {
            var model = await _apiKeyRepo.GetBySystanId(systanId);
            if (model == null)
                throw new Exception("ApiKey was not found.");

            model.StatusReason = statusReason;
            model.StatusReasonTitle = statusReasonTitle;
            return await _apiKeyRepo.Update(model);
        }

        public async Task<ApiKeySetting?> CreateSetting(string apiKeySystanId, ApiKeySetting setting)
        {
            var model = await _apiKeyRepo.GetSettingBySystanId(setting.SystanId);
            if (model != null)
                throw new Exception("ApiKey Setting Already Exists.");


            var apiKey = await _apiKeyRepo.GetBySystanId(apiKeySystanId);
            if (apiKey == null)
                throw new Exception("Related ApiKey was not found.");

            setting.ApiKeyId = apiKey.Id;
            await _apiKeyRepo.CreateSetting(setting);

            return setting;
        }

        public async Task DeleteApiKey(string systanId)
        {
            await _apiKeyRepo.DeleteBySystanId(systanId);
        }

        public async Task DeleteSetting(string systanId)
        {
            await _apiKeyRepo.DeleteSettingBySystanId(systanId);
        }

        public async Task<ApiKey> UpdateApiKey(ApiKey apiKey)
        {
            if (apiKey.SystanId == null)
                throw new Exception("SystanId is undefined.");

            var model = await _apiKeyRepo.GetBySystanId(apiKey.SystanId);
            if (model == null)
                throw new Exception("ApiKey was not found.");

            model.Key = apiKey.Key;
            model.ServiceId = apiKey.ServiceId;
            await _apiKeyRepo.Update(model);

            return model;
        }

        public async Task<ApiKeySetting> UpdateSetting(ApiKeySetting apiKeySetting)
        {
            var model = await _apiKeyRepo.GetSettingBySystanId(apiKeySetting.SystanId);
            if (model == null)
                throw new Exception("ApiKey Setting was not found.");

            model.Key = apiKeySetting.Key;
            model.Value = apiKeySetting.Value;
            return await _apiKeyRepo.UpdateSetting(model);
        }
    }
}

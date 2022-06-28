using Systan.ApiKeyManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Interfaces
{
    public interface IApiKeyService
    {
        Task<ApiKey> AddApiKey(ApiKey model);
        Task<ApiKey> UpdateApiKey(ApiKey apiKey);
        Task<ApiKeySetting> UpdateSetting(ApiKeySetting apiKeySetting);
        Task<ApiKey?> ChangeApiKeyStatusReason(string systanId, int statusReason, string statusReasonTitle);
        Task DeleteApiKey(string systanId);
        Task<ApiKeySetting?> CreateSetting(string apiKeySystanId, ApiKeySetting setting);
        Task DeleteSetting(string systanId);
    }
}

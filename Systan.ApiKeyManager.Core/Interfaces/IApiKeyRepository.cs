using Systan.ApiKeyManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Interfaces
{
    public interface IApiKeyRepository : IBaseRepository<ApiKey>
    {
        bool ApiKeyExists(string systanId);
        Task<ApiKey> DeleteBySystanId(string systanId);
        Task<ApiKey?> GetBySystanId(string systanId);
        Task<ApiKeySetting> CreateSetting(ApiKeySetting setting);
        Task<ApiKeySetting> UpdateSetting(ApiKeySetting setting);
        Task<ApiKeySetting> GetSettingBySystanId(string systanId);
        Task<ApiKeySetting> DeleteSettingBySystanId(string systanId);
    }
}

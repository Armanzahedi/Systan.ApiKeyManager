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
        Task<ApiKey> UpdateApiKey(ApiKey model);
        Task DeleteApiKey(string systanId);
    }
}

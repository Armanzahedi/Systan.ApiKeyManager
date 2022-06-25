using Systan.ApiKeyManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.Core.Interfaces
{
    public interface IApiKeySettingRepository: IBaseRepository<ApiKeySetting>
    {
        Task<ApiKeySetting?> GetBySystanId(string systanId);
    }
}

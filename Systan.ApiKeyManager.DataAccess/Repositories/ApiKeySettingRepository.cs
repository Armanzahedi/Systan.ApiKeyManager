using Microsoft.EntityFrameworkCore;
using Systan.ApiKeyManager.Core.Entities;
using Systan.ApiKeyManager.Core.Interfaces;
using Systan.ApiKeyManager.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.DataAccess.Repositories
{
    public class ApiKeySettingRepository : BaseRepository<ApiKeySetting>, IApiKeySettingRepository
    {
        public ApiKeySettingRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<ApiKeySetting?> GetBySystanId(string systanId)
        {
            return await GetDefaultQuery().FirstOrDefaultAsync(a => a.SystanId == systanId);
        }
    }
}

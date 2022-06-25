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
    public class ApiKeyRepository : BaseRepository<ApiKey>, IApiKeyRepository
{
        private readonly AppDbContext _context;
        private readonly IApiKeySettingRepository _apiKeySettingRepo;
        public ApiKeyRepository(AppDbContext context, IApiKeySettingRepository apiKeySettingRepo) : base(context)
        {
            _context = context;
            _apiKeySettingRepo = apiKeySettingRepo;
        }


        public bool ApiKeyExists(string systanId)
        {
            return base.GetDefaultQuery().Any(a => a.SystanId == systanId);
        }

        

        public async Task<ApiKey> DeleteBySystanId(string systanId)
        {
            var apiKey = await GetBySystanId(systanId);
            if (apiKey == null)
                throw new Exception("ApiKey doesn't exists");

           return await base.Delete(apiKey);
        }

        public async Task<ApiKey?> GetBySystanId(string systanId)
        {
            return await GetDefaultQuery().FirstOrDefaultAsync(a => a.SystanId == systanId);
        }

        public async Task<ApiKeySetting> CreateSetting(ApiKeySetting setting)
        {
            return await this._apiKeySettingRepo.Add(setting);
        }
        public async Task<ApiKeySetting> UpdateSetting(ApiKeySetting setting)
        {
            return await this._apiKeySettingRepo.Update(setting);
        }
        public async Task<ApiKeySetting> DeleteSettingBySystanId(string systanId)
        {

            var setting = await this._apiKeySettingRepo.GetBySystanId(systanId);
            if (setting == null)
                throw new Exception("ApiKey Setting was not found");

            return await this._apiKeySettingRepo.Delete(setting);
        }

        public async Task<ApiKeySetting> GetSettingBySystanId(string systanId)
        {
            return await this._apiKeySettingRepo.GetBySystanId(systanId);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using Systan.ApiKeyManager.Core.Interfaces;
using Systan.ApiKeyManager.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.DataAccess
{
    public static class Startup
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services)
        {
            #region Register Repositories
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IApiKeyRepository, ApiKeyRepository>();
            services.AddScoped<IApiKeySettingRepository, ApiKeySettingRepository>();
            #endregion

            return services;
        }
    }
}

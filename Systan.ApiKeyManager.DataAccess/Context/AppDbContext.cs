using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Systan.ApiKeyManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Systan.ApiKeyManager.DataAccess.Context
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }

        public DbSet<ApiKey>? ApiKey { get; set; }
        public DbSet<ApiKeySetting>? ApiKeySetting { get; set; }

    }
}

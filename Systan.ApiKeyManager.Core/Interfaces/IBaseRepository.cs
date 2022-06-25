using Systan.ApiKeyManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Systan.ApiKeyManager.Core.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAll();
        Task<int> GetCount();
        Task<List<T>> GetListPaged(int pageNumber, int itemsPerPage);
        Task<T?> GetById(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> AddOrUpdate(T entity);
        Task<T?> Delete(int id);
        Task<T> Delete(T entity);
        Task<T?> Remove(int id);
    }
}

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
    public class BaseRepository<T> : IBaseRepository<T>
           where T : BaseEntity
    {
        private readonly AppDbContext _context;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public virtual async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().Where(x => x.IsDeleted == false).ToListAsync();
        }

        public virtual async Task<int> GetCount()
        {
            return await _context.Set<T>().CountAsync();
        }

        public virtual async Task<List<T>> GetListPaged(int pageNumber = 1, int itemsPerPage = 10)
        {
            var entity = await _context.Set<T>().Skip((pageNumber - 1) * itemsPerPage).Where(e => e.IsDeleted == false)
                .Take(itemsPerPage).ToListAsync();

            return entity;

        }

        public virtual async Task<T?> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> Add(T entity)
        {
            entity.CreatedAt = DateTime.Now;   
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
        public virtual async Task<T> Update(T entity)
        {
            var oldEntity = await _context.Set<T>().FindAsync(entity.Id);

            if (oldEntity != null)
            {
                entity.CreatedAt = oldEntity.CreatedAt;
                _context.Entry(oldEntity).State = EntityState.Detached;
                _context.SaveChanges();
            }
            
            entity.UpdatedAt = DateTime.Now;
            
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return entity;
        }
        public virtual async Task<T> AddOrUpdate(T entity)
        {
            int? a = await _context.Set<T>().Select(e => e.Id).FirstOrDefaultAsync(e => e == entity.Id);

            if (a != null)
                return await Update(entity);
            else
                return await Add(entity);
        }
        public virtual async Task<T?> Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            entity!.IsDeleted = true;
            return await Update(entity);
        }
        public virtual async Task<T> Delete(T entity)
        {
            entity.IsDeleted = true;
            return await Update(entity);
        }
        public virtual async Task<T?> Remove(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }

            return entity;
        }

        protected IQueryable<T> GetDefaultQuery()
        {
            return _context.Set<T>().Where(m => m.IsDeleted == false).AsQueryable();
        }

    }
}

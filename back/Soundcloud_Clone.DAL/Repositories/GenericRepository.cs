using Microsoft.EntityFrameworkCore;
using Soundcloud_Clone.DAL.Enitites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Soundcloud_Clone.DAL.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class, IBaseEntity
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            return (await _context.SaveChangesAsync() != 0);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().AsNoTracking();
        }
        public virtual async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var e = await GetByIdAsync(id);
            if (e != null)
            {
                _context.Set<TEntity>().Remove(e);
                return await _context.SaveChangesAsync() != 0;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            var e = _context.Set<TEntity>().Update(entity);
            if (e != null)
            {
                return await _context.SaveChangesAsync() != 0;
            }
            return false;
        }
    }
}

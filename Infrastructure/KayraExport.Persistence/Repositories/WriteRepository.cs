using KayraExport.Application.Repositories;
using KayraExport.Domain.Entities.Common;
using KayraExport.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        readonly KayraExportContext _context;

        public WriteRepository(KayraExportContext context)
            => _context = context;


        public DbSet<T> Table 
            => _context.Set<T>();


        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entityEntry = await _context.AddAsync(entity);
            
            return entityEntry.State == EntityState.Added;
        }


        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);

            return true;
        }


        public bool Remove(T entity)
        {
            EntityEntry<T> entityEntry = _context.Remove(entity);

            return entityEntry.State == EntityState.Deleted;
        }


        public async Task<bool> RemoveAsync(int id)
        {
            T? entity = await Table.SingleOrDefaultAsync(e => e.Id == id);

            if (entity == null) 
                return false;

            return Remove(entity);
        }


        public bool Update(T entity)
        {
            EntityEntry<T> entityEntry = Table.Update(entity);

            return entityEntry.State == EntityState.Modified;   
        }


        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}

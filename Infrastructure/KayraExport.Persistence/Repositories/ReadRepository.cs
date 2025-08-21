using KayraExport.Application.Repositories;
using KayraExport.Domain.Entities;
using KayraExport.Domain.Entities.Common;
using KayraExport.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
    {
        readonly KayraExportContext _context;

        public ReadRepository(KayraExportContext context)
            => _context = context;
        


        public DbSet<T> Table 
            => _context.Set<T>();


        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null)
            => expression == null ? await Table.AsNoTracking().ToListAsync() : await Table.Where(expression).AsNoTracking().ToListAsync();


        public async Task<T?> GetByIdAsync(int id)
            => await Table.FindAsync(id);


        public async Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression)
            => await Table.SingleOrDefaultAsync(expression);


        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
            => Table.Where(expression);
    }
}

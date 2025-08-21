using KayraExport.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Repositories
{
    public interface IReadRepository<T> : IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null);

        IQueryable<T> Where(Expression<Func<T, bool>> expression);

        Task<T?> GetSingleAsync(Expression<Func<T, bool>> expression);

        Task<T?> GetByIdAsync(int id);
    }
}

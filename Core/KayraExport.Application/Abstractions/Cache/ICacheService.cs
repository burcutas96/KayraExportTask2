using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Abstractions.Cache
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);

        Task SetAsync<T>(string key, T value, DateTime? expirationTime = null);

        Task RemoveAsync(string key);

        Task RemoveRangeAsync(params string[] keys);
    }
}

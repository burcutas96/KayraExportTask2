using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Application.Abstractions.Cache
{
    // Sadece cache'lenebilecek query’leri işaretliyoruz.
    public interface ICacheableQuery
    {
        string GetCacheKey() => GetType().Name;

    }
}

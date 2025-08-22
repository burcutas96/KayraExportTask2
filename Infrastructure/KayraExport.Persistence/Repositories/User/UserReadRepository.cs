using KayraExport.Application.Repositories;
using KayraExport.Domain.Entities;
using KayraExport.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KayraExport.Persistence.Repositories
{
    public class UserReadRepository : ReadRepository<User>, IUserReadRepository
    {
        public UserReadRepository(KayraExportContext context) : base(context)
        {
        }
    }
}

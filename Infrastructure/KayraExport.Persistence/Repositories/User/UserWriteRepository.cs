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
    public class UserWriteRepository : WriteRepository<User>, IUserWriteRepository
    {
        public UserWriteRepository(KayraExportContext context) : base(context)
        {
        }
    }
}

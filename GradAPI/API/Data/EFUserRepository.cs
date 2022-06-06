using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public class EFUserRepository: RepositoryBase<AppUser>, IUserRepository
    {
        public EFUserRepository(DataContext appDbContext)
            : base(appDbContext)
        {
        }
        
    }
}
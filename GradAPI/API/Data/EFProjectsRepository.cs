using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public class EFProjectsRepository: RepositoryBase<Projects>, IProjectsRepository
    {
        public EFProjectsRepository(DataContext appDbContext)
            : base(appDbContext)
        {
        }
        
    }
}
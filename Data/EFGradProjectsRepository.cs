using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public class EFGradProjectsRepository : RepositoryBase<GradProjects>, IGradProjects
    {
        public EFGradProjectsRepository(DataContext appDbContext)
            : base(appDbContext)
        {
        }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public class EFExperienceRepository: RepositoryBase<Experiences>, IExperienceRepository
    {
        public EFExperienceRepository(DataContext appDbContext)
            : base(appDbContext)
        {
        }
        
    }
}
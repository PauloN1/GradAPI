using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public class EFHobbiesRepository: RepositoryBase<Hobbies>, IHobbiesRepository
    {
        public EFHobbiesRepository(DataContext appDbContext)
            : base(appDbContext) {}
    }
}

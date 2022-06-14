using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
  public class EFGradHobbiesRepository : RepositoryBase<GradHobbies>, IGradHobbies
  {
    public EFGradHobbiesRepository(DataContext appDbContext)
        : base(appDbContext) {}
  }
}
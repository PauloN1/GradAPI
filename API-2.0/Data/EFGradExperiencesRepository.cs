using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
  public class EFGradExperiencesRepository : RepositoryBase<GradExperiences>, IGradExperiences
  {
    public EFGradExperiencesRepository(DataContext appDbContext)
        : base(appDbContext) {}
  }
}
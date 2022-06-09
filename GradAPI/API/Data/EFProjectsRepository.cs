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
    { }

    public Projects GetByName(string name)
    {
      return _appDbContext.Projects.FirstOrDefault(project => project.Name.ToLower() == name.Trim().ToLower());
    }

    public List<int> GetGradsIDsUsingProjectId(int projectID)
    {
      IEnumerable<GradProjects> gradProject = _appDbContext.GradProjects.Where(gradProj => gradProj.ProjectsId == projectID);

      List<int> result = new List<int>();

      foreach (GradProjects item in gradProject)
      {
        result.Add(item.GradId);
      }

      return result;
    }
  }
}
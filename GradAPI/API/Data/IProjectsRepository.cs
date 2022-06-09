using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
  public interface IProjectsRepository : IRepositoryBase<Projects>
  {
    Projects GetByName(string name);
    List<int> GetGradsIDsUsingProjectId(int projectID);
    List<Grads> GetGradsUsingGradIDs(List<int> gradIDs);
  }
}
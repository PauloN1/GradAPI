using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public interface IExperienceRepository : IRepositoryBase<Experiences>
    {
        Experiences GetByName(string name);
        List<int> GetGradsIDsUsingExperienceId(int experienceID);
        List<Grads> GetGradsUsingGradIDs(List<int> gradIDs);

    }
}
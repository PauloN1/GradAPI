using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public interface IRepositoryWrapper
    {
        //Adapter Design Pattern : Achieves the open-closed principles

        IHobbiesRepository Hobbies{get;}
        IUserRepository Grads{get;}
        IExperienceRepository Experiences{get;}
        IProjectsRepository Projects{get;}
        IGradProjects GradProjects{get;}
    }
}
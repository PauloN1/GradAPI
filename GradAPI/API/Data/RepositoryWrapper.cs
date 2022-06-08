using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        //Singleton Pattern : Instantiates only once

        private readonly DataContext _appDbContext;
        private IHobbiesRepository hobbies;
        private IUserRepository grads;
        private IExperienceRepository experience;
        private IProjectsRepository projects;
        public RepositoryWrapper(DataContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IHobbiesRepository Hobbies
        {
            get
            {
                if (hobbies == null)
                    hobbies = new EFHobbiesRepository(_appDbContext);

                return hobbies;
            }

        }
          public IUserRepository Grads
        {
            get
            {
                if (grads == null)
                    grads = new EFUserRepository(_appDbContext);

                return grads;
            }

        }
        public IExperienceRepository Experiences
        {
            get
            {
                if (experience == null)
                    experience = new EFExperienceRepository(_appDbContext);

                return experience;
            }

        }
        public IProjectsRepository Projects
        {
            get
            {
                if (projects == null)
                    projects = new EFProjectsRepository(_appDbContext);

                return projects;
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
         private DataContext _appDbContext;
        private IHobbiesRepository hobbies;
        private IUserRepository appusers;
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
          public IUserRepository AppUsers
        {
            get
            {
                if (appusers == null)
                    appusers = new EFUserRepository(_appDbContext);

                return appusers;
            }

        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Data
{
    public class EFExperienceRepository : RepositoryBase<Experiences>, IExperienceRepository
    {
        public EFExperienceRepository(DataContext appDbContext)
            : base(appDbContext)
        { }

        public Experiences GetByName(string name)
        {
            return _appDbContext.Experiences.FirstOrDefault(experience => experience.Name.ToLower() == name.Trim().ToLower());
        }

        public List<int> GetGradsIDsUsingExperienceId(int experienceID)
        {
            IEnumerable<GradExperiences> gradEx = _appDbContext.GradExperiences.Where(gradExp => gradExp.ExperiencesId == experienceID);

            List<int> resulting = new List<int>();

            foreach (GradExperiences entry in gradEx)
            {
                resulting.Add(entry.GradId);
            }

            return resulting;
        }

        public List<Grads> GetGradsUsingGradIDs(List<int> gradIDs)
        {
            List<Grads> resulting = new List<Grads>();

            foreach (int gradID in gradIDs)
            {
                Grads validGrad = _appDbContext.Grads.FirstOrDefault(grad => grad.Id == gradID);
                if (validGrad != null)
                {
                    resulting.Add(validGrad);
                }
            }

            return resulting;
        }
    }
}
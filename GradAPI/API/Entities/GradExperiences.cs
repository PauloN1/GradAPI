using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class GradExperiences
    {
        public int Id{get;set;}
        public int GradId{get;set;}
        public Grads Grad{get;set;}

        public int ExperienceId{get;set;}
        public Experiences Experiences{get;set;}

        public int Duration{get;set;}
    }
}
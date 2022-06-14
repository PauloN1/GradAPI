using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Models
{
    public class ExperienceModel
    {
        public GradUsersDTO Grad {get;set;}
        public List<GradExperienceDTO> GradExperiences{get;set;}
    }
}
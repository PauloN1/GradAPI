using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Models
{
    public class ProjectModel 
    {
        public GradUsersDTO Grad {get;set;}
        public List<GradProjectsDTO> UserProjects{get;set;}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;

namespace API.Models
{
    public class HobbiesModel
    {
        public GradUsersDTO Grad {get;set;}
        public List<GradHobbiesDTO> GradHobbies{get;set;}
    }
}
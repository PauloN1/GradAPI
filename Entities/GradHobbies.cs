using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class GradHobbies
    {
        public int Id{get;set;}
        public int GradId{get;set;}
        public Grads Grad{get;set;}

        public int HobbiesId{get;set;}
        public Hobbies Hobbies{get;set;}

        public int Duration{get;set;}
    }
}
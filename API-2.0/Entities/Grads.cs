using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Grads
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName{get;set;}
        public string Email{get;set;}
        public string Country{get;set;}
        public string Branch{get;set;}
        public int Age{get;set;}
    }
}
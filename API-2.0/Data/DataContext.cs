using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options){

        }
        //Creating tables in the database using dbsets
        public DbSet<Grads> Grads {get;set;}
        
        public DbSet<Hobbies> Hobbies {get;set;}    
        public DbSet<GradHobbies> GradHobbies{get;set;}

        public DbSet<Projects> Projects {get;set;}    
        public DbSet<GradProjects> GradProjects{get;set;}

        public DbSet<Experiences> Experiences {get;set;}    
        public DbSet<GradExperiences> GradExperiences{get;set;}

    }
}
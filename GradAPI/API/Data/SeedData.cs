using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public static class SeedData
    {
        public static void EnsurePopulate(IApplicationBuilder app)
        {
            DataContext context = app.ApplicationServices.CreateScope()
                 .ServiceProvider.GetRequiredService<DataContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Hobbies.Any())
            {
                context.Hobbies.AddRange
                    (
                        new Hobbies { Name = "Sporting", Description = "Playing outdoors and watching soccer games" },
                        new Hobbies { Name = "Reading", Description = "Reading Novels" },
                        new Hobbies { Name = "Running or Jogging", Description = "Health excercises" }
                    /* new Hobbies { Name = "Running or Jogging", UserId=3 },
                     new Hobbies { Name = "Sporting", UserId= 3},
                     new Hobbies { Name = "Programming", UserId = 3 },
                     new Hobbies { Name = "Family Gathering", UserId=3 },
                     new Hobbies { Name = "Cuddling", UserId= 1},
                     new Hobbies { Name = "Socializing", UserId = 1 },
                     new Hobbies { Name = "Running or Jogging", UserId=1 }*/

                    );
            }
            /*
            Experience data
            */
            if (!context.Experiences.Any())
            {
                context.Experiences.AddRange(
                    new Experiences { Name = "Java", Description = "SpringBoot", },
                    new Experiences { Name = "Javascript", Description = "Web", },
                    new Experiences { Name = "Serverless BackEnd", Description = "Node.js and Javascript" },
                    new Experiences { Name = "AWS Load Balancing", Description = "Backend and cloud" },
                    new Experiences { Name = "AWS Hosting", Description = "Web and cloud" },
                    new Experiences { Name = "AWS Analytics", Description = "Cloud and data" },
                    new Experiences { Name = "AWS Application Integration", Description = "Cloud and integration" },
                    new Experiences { Name = "Ruby", Description = "Object-Orientated where everything is an object" }
                );
            }

            /* 
            Grads
            */
            if (!context.Grads.Any())
            {
                context.Grads.AddRange(
                    new Grads { FirstName = "Johnny", LastName = "Cash", Email = "jc@gmail.com", Country = "RSA", Branch = "JHB", Age = 33 },
                    new Grads { FirstName = "Mila", LastName = "Kunez", Email = "mc@gmail.com", Country = "IND", Branch = "PUNE", Age = 66 },
                    new Grads { FirstName = "Blessing", LastName = "Kumalo", Email = "bc@hotmail.com", Country = "RSA", Branch = "PTA", Age = 40 },
                    new Grads { FirstName = "Sam", LastName = "Stark", Email = "ss@gmail.com", Country = "RSA", Branch = "CT", Age = 20 },
                    new Grads { FirstName = "Marc", LastName = "Johnson", Email = "mj@gmail.com", Country = "RSA", Branch = "CT", Age = 26 }
                );
            }

            context.SaveChanges();

            /* 
            Grad/Experience
                */

            if (!context.GradExperiences.Any())
            {
                context.GradExperiences.AddRange(
                     new GradExperiences { GradId = 1, ExperiencesId = 1, Duration = 2 },
                     new GradExperiences { GradId = 1, ExperiencesId = 2, Duration = 2 },
                     new GradExperiences { GradId = 2, ExperiencesId = 2, Duration = 2 }
                 );
            }


            context.SaveChanges();
        }
    }
}
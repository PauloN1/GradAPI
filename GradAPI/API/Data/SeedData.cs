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
       public static void EnsurePopulate(IApplicationBuilder app){
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
                        new Hobbies { Name = "Sporting",  Description = "Playing outdoors and watching soccer games"},
                        new Hobbies { Name = "Reading", Description = "Reading Novels"},
                        new Hobbies { Name = "Running or Jogging", Description = "Health excercises"}
                       /* new Hobbies { Name = "Running or Jogging", UserId=3 },
                        new Hobbies { Name = "Sporting", UserId= 3},
                        new Hobbies { Name = "Programming", UserId = 3 },
                        new Hobbies { Name = "Family Gathering", UserId=3 },
                        new Hobbies { Name = "Cuddling", UserId= 1},
                        new Hobbies { Name = "Socializing", UserId = 1 },
                        new Hobbies { Name = "Running or Jogging", UserId=1 }*/
        
                    );
            }
            if(!context.Experiences.Any()){

                context.Experiences.AddRange(
                    new Experiences{
                        Name = "Java",
                        Description = "SpringBoot",
                        
                    },
                    new Experiences{
                        Name = "Serverless BankEnd",
                        Description = "Node.js and Javascript"
                    }
                );

            }
            context.SaveChanges();
       } 
    }
}
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
                        new Hobbies { Name = "Sporting", UserId= 1},
                        new Hobbies { Name = "Reading", UserId = 1 },
                        new Hobbies { Name = "Running or Jogging", UserId=2 },
                        new Hobbies { Name = "Music and Social", UserId= 2},
                        new Hobbies { Name = "Reading", UserId = 3 },
                        new Hobbies { Name = "Running or Jogging", UserId=3 },
                        new Hobbies { Name = "Sporting", UserId= 3},
                        new Hobbies { Name = "Programming", UserId = 3 },
                        new Hobbies { Name = "Family Gathering", UserId=3 },
                        new Hobbies { Name = "Cuddling", UserId= 1},
                        new Hobbies { Name = "Socializing", UserId = 1 },
                        new Hobbies { Name = "Running or Jogging", UserId=1 }
        
                    );
            }
            context.SaveChanges();
       } 
    }
}
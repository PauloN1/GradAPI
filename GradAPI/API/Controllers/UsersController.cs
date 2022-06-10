using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.DTOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : BaseAPIController
    {
         private readonly IRepositoryWrapper _context;

        public UsersController(IRepositoryWrapper context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Grads>> GetUsers(){
            return _context.Grads.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Grads> GetUser(int id){
           return _context.Grads.GetById(id);
        }
        [HttpGet("projects")]
        public  ActionResult<ProjectModel> GetUserWithProjects(string UserEmail){
            try{
           var user = _context.Grads.GetAll().ToList()
                .FirstOrDefault(s => s.Email == UserEmail);

                if(user == null)
                {
                    return BadRequest($"User with email: {UserEmail} does not exit");
                }

                var projectIds = _context.GradProjects.GetAll().ToList()
                .Where(s => s.GradId == user.Id);
                
                List<GradProjectsDTO> _UserProjects = new  List<GradProjectsDTO>();
                
                foreach (var item in projectIds)
                {
                    var curproject = _context.Projects.GetById(item.ProjectsId);
                    _UserProjects.Add(
                        new GradProjectsDTO{
                            Name = curproject.Name,
                            Description = curproject.Description,
                            Duration = item.Duration
                        }
                    );
                }

                return new ProjectModel {
                    Grad = new GradUsersDTO{
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Email = user.Email,
                        Age = user.Age,
                        Branch = user.Branch,
                        Country = user.Country
                    },
                    UserProjects = _UserProjects
                };
            }catch(Exception ex){
                return BadRequest(ex.Message);
            }
            return BadRequest("Unkown Error occured!");
        }
    }
}
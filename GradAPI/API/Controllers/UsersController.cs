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

    public UsersController(IRepositoryWrapper context) {
      _context = context;
    }

    /* Get all Grads */
    [HttpGet]
    public ActionResult<IEnumerable<Grads>> GetUsers() {
      return _context.Grads.GetAll().ToList();
    }

    /* Get Grads by ID*/
    [HttpGet("{id}")]
    public ActionResult<Grads> GetUser(int id) {
      return _context.Grads.GetById(id);
    }

    /* Get Projects worked in by given grad */
    [HttpGet("projects")]
    public  ActionResult<ProjectModel> GetUserWithProjects(string UserEmail) {

      try {

      var grads = _context.Grads.GetAll().ToList().FirstOrDefault(grad => grad.Email == UserEmail);

        if(grads == null)
        {
          return StatusCode(200, "User with email " + UserEmail + " does not exist!");
        }

        var gradProjectId = _context.GradProjects.GetAll().ToList().Where(gradProject => gradProject.GradId == grads.Id);

        List<GradProjectsDTO> GradProjects = new  List<GradProjectsDTO>();

        foreach (var item in gradProjectId)
        {
          var project = _context.Projects.GetById(item.ProjectsId);
          GradProjects.Add(
            new GradProjectsDTO{
              Name = project.Name,
              Description = project.Description,
              Duration = item.Duration
            }
          );
        }

        return new ProjectModel {
            Grad = new GradUsersDTO{
              FirstName = grads.FirstName,
              LastName = grads.LastName,
              Email = grads.Email,
              Age = grads.Age,
              Branch = grads.Branch,
              Country = grads.Country
            },
            GradProjects = GradProjects
        };
      }
      catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    /* Get Hobbies of given grad */
    [HttpGet("hobbies")]
    public  ActionResult<HobbiesModel> GetUserWithHobbies(string UserEmail) {

      try {

      var grads = _context.Grads.GetAll().ToList().FirstOrDefault(grad => grad.Email == UserEmail);

        if(grads == null)
        {
          return StatusCode(200, "User with email " + UserEmail + " does not exist!");
        }

        var gradHobbyId = _context.GradHobbies.GetAll().ToList().Where(gradHobby => gradHobby.GradId == grads.Id);

        List<GradHobbiesDTO> GradHobby = new  List<GradHobbiesDTO>();

        foreach (var item in gradHobbyId)
        {
          var hobby = _context.Hobbies.GetById(item.HobbiesId);
          GradHobby.Add(
            new GradHobbiesDTO{
              Name = hobby.Name,
              Description = hobby.Description,
            }
          );
        }

        return new HobbiesModel {
            Grad = new GradUsersDTO{
              FirstName = grads.FirstName,
              LastName = grads.LastName,
              Email = grads.Email,
              Age = grads.Age,
              Branch = grads.Branch,
              Country = grads.Country
            },
            GradHobbies = GradHobby
        };
      }
      catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    /* Get Experiences of given grad */
    [HttpGet("experiences")]
    public  ActionResult<ExperienceModel> GetUserWithExperiences(string UserEmail) {

      try {

      var grads = _context.Grads.GetAll().ToList().FirstOrDefault(grad => grad.Email == UserEmail);

        if(grads == null)
        {
          return StatusCode(200, "User with email " + UserEmail + " does not exist!");
        }

        var gradExId = _context.GradExperiences.GetAll().ToList().Where(gradEx => gradEx.GradId == grads.Id);

        List<GradExperienceDTO> GradExp = new  List<GradExperienceDTO>();

        foreach (var item in gradExId)
        {
          var exp = _context.Experiences.GetById(item.ExperiencesId);
          GradExp.Add(
            new GradExperienceDTO{
              Name = exp.Name,
              Description = exp.Description,
              Duration = item.Duration
            }
          );
        }

        return new ExperienceModel {
            Grad = new GradUsersDTO{
              FirstName = grads.FirstName,
              LastName = grads.LastName,
              Email = grads.Email,
              Age = grads.Age,
              Branch = grads.Branch,
              Country = grads.Country
            },
            GradExperiences = GradExp
        };
      }
      catch(Exception ex) {
        return BadRequest(ex.Message);
      }
    }

    /* Add Project under Grads Project history */
    //[HttpPost("addProject")]
  }
}
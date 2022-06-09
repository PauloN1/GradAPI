using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Entities;
using API.Data;
using API.DTOs;

namespace API.Controllers
{
  [Route("api/[controller]")]
  public class ProjectController : BaseAPIController
  {
    private readonly ILogger<ProjectController> _logger;
    private readonly IRepositoryWrapper _context;

    public ProjectController(ILogger<ProjectController> logger, IRepositoryWrapper context)
    {
      _logger = logger;
      _context = context;
    }

    /* Get All Projects*/
    [HttpGet]
    public ActionResult<IEnumerable<GradProjectsDTO>> GetProjects()
    {
      var projects = _context.Projects.GetAll().ToList();
      var projectsDTO = new List<GradProjectsDTO>();
      foreach (var item in projects)
      {
        projectsDTO.Add(new GradProjectsDTO
        {
          Name = item.Name,
          Description = item.Description
        });
      }
      return projectsDTO.ToList();
    }

    /* Get Projects By ID*/
    [HttpGet("{id}")]
    public ActionResult<GradProjectsDTO> GetProjects(int id)
    {
      Projects projects = _context.Projects.GetById(id);

      if (projects == null)
      {
        return StatusCode(200, "Project with Id " + id + " does not exist!");
      }
      else
      {
        return new GradProjectsDTO
        {
          Name = projects.Name,
          Description = projects.Description
        }; ;
      }
    }

    /* Get all grads by project */
    [HttpGet("grads/{projectName}")]
    public ActionResult<List<GradUsersDTO>> GetGrads(string projectName)
    {
      int statusCode = -1;
      string errorMessage = "";
      int project = -1;
      List<int> gradIDs = new List<int>();
      List<Grads> grads = new List<Grads>();

      try {

        project = _context.Projects.GetByName(projectName).Id;
        gradIDs = _context.Projects.GetGradsIDsUsingProjectId(project);
        grads = _context.Projects.GetGradsUsingGradIDs(gradIDs);

        List<GradUsersDTO> gradUsersDTOs = new List<GradUsersDTO>();

        foreach (var item in grads)
        {
          gradUsersDTOs.Add(new GradUsersDTO
          {
            FirstName = item.FirstName,
            LastName = item.LastName,
            Email = item.Email,
            Country = item.Country,
            Age = item.Age
          });
        }

        return gradUsersDTOs;
      }
      catch {

        if (project == -1) {

          if (statusCode == -1) {

            statusCode = 500;
            errorMessage = "No project with given name";
          }
        }
        if (!(gradIDs.Count > 0)) {

          if (statusCode == -1) {

            statusCode = 200;
            errorMessage = "No grad ids: No grads with project '" + projectName + "'";
          }
        }
        if (!(grads.Count > 0)) {

          if (statusCode == -1) {

            statusCode = 200;
            errorMessage = "No grads: No grads with project '" + projectName + "'";
          }
        }
      }
      finally {

        if (statusCode == -1)
        {
          statusCode = 500;
          errorMessage = "Grads could not be retrieved!";
        }
      }
      return StatusCode(statusCode, errorMessage);
    }

    /* Add New project */
    [HttpPost("addProject")]
    public ActionResult<GradProjectsDTO> AddProject(GradActivitiesDTO projectDTO)
    {
      int statusCode = -1;
      string errorMessage = "";

      Projects project = new Projects
      {
          Name = projectDTO.Name,
          Description = projectDTO.Description
      };

      try {

        if (project.Name == null || project.Name == "" || project.Description == null || project.Description == "") {

          statusCode = 400;
          errorMessage = "Input data missing fields!";
        }
        else {

          _context.Projects.Create(project);

          if (_context.Projects.SaveChanges() > 0)
          {
            return CreatedAtAction(nameof(AddProject), project);
          }
          else
          {
            statusCode = 500;
            errorMessage = "Could not add project, try again.";
          }
        }
      }
      catch {

        if (statusCode == -1) {

          statusCode = 500;
          errorMessage = "Could not add project, try again.";
        }
      }
      finally {

        if (statusCode == -1) {

          statusCode = 500;
          errorMessage = "Could not add project, try again.";
        }
      }
      return StatusCode(statusCode, errorMessage);
    }
  }
}
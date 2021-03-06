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
    //[Authorize]
    [Route("api/[controller]")]
    public class ExperienceController : BaseAPIController
    {
        private readonly ILogger<ExperienceController> _logger;

        private readonly IRepositoryWrapper _context;

        public ExperienceController(ILogger<ExperienceController> logger, IRepositoryWrapper context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<GradExperienceDTO>> GetExperiences()
        {

            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not authorised. Please log in here: https://localhost:5001/Account/Login");
            }

            var experiences = _context.Experiences.GetAll().ToList();
            var experiencesDTO = new List<GradExperienceDTO>();
            foreach (var item in experiences)
            {
                experiencesDTO.Add(new GradExperienceDTO
                {
                    Name = item.Name,
                    Description = item.Description
                });
            }
            return experiencesDTO.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<GradExperienceDTO> GetExperience(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not authorised. Please log in here: https://localhost:5001/Account/Login");
            }

            Experiences experience = _context.Experiences.GetById(id);

            if (experience == null)
            {
                return StatusCode(200, "Experience with Id " + id + " does not exist!");
            }
            else
            {
                return new GradExperienceDTO
                {
                    Name = experience.Name,
                    Description = experience.Description
                }; ;
            }
        }

        // get all grads based on experience name
        [HttpGet("grads/{experienceName}")]
        public ActionResult<List<GradUsersDTO>> GetGrads(string experienceName)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not authorised. Please log in here: https://localhost:5001/Account/Login");

            }
            int statusCode = -1;
            string message = "";
            int experience = -1; //state for error
            List<int> gradIDs = new List<int>(); //state for error
            List<Grads> grads = new List<Grads>(); //state for error
            try
            {
                // "Java" -> Experience id (single)
                experience = _context.Experiences.GetByName(experienceName).Id;

                // Experience id -> Grad id list 
                gradIDs = _context.Experiences.GetGradsIDsUsingExperienceId(experience);

                // Grad id list -> grads
                grads = _context.Experiences.GetGradsUsingGradIDs(gradIDs);

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
            catch
            {
                if (experience == -1)
                {
                    if (statusCode == -1)
                    {
                        statusCode = 500;
                        message = "No experience with given name";
                    }
                }
                if (!(gradIDs.Count > 0))
                {
                    if (statusCode == -1)
                    {
                        statusCode = 200;
                        message = "No grad ids: No grads with experience '" + experienceName + "'";
                    }
                }
                if (!(grads.Count > 0))
                {
                    if (statusCode == -1)
                    {
                        statusCode = 200;
                        message = "No grads: No grads with experience '" + experienceName + "'";
                    }
                }
            }
            finally
            {
                if (statusCode == -1)
                {
                    statusCode = 500;
                    message = "finally: Grads could not be retrieved!";
                }
            }
            return StatusCode(statusCode, message);

        }

        // add 
        [HttpPut("add")]
        public ActionResult<GradExperienceDTO> AddExperience(GradActivitiesDTO experienceDTO)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not authorised. Please log in here: https://localhost:5001/Account/Login");
            }

            int statusCode = -1;
            string message = "";

            Experiences experience = new Experiences
            {
                Name = experienceDTO.Name,
                Description = experienceDTO.Description
            };

            try
            {
                // all fields entered
                if (experience.Name == null || experience.Name == "" || experience.Description == null || experience.Description == "")
                {
                    statusCode = 400;
                    message = "Input data missing fields!";
                }
                else
                {
                    // add
                    _context.Experiences.Create(experience);

                    if (_context.Experiences.SaveChanges() > 0)
                    {
                        return CreatedAtAction(nameof(AddExperience), experience);
                    }
                    else
                    {
                        statusCode = 500;
                        message = "Could not add experience, try again.";
                    }
                }
            }
            catch
            {
                if (statusCode == -1)
                {
                    statusCode = 500;
                    message = "Could not add experience, try again.";
                }
            }
            finally
            {
                if (statusCode == -1)
                {
                    statusCode = 500;
                    message = "Could not add experience, try again.";
                }
            }
            return StatusCode(statusCode, message);
        }

        // update
        [HttpPatch("update/{id}")]
        public ActionResult EditExperience(int id, GradExperienceDTO experienceDTO)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not authorised. Please log in here: https://localhost:5001/Account/Login");
            }

            int statusCode = -1;
            string message = "";

            if (!int.TryParse(id.ToString(), out id))
            {
                if (statusCode == -1)
                {
                    statusCode = 400;
                    message = "Invalid Id presented";
                }
            }

            try
            {
                Experiences curExperience = _context.Experiences.GetById(id);
                if (curExperience == null)
                {
                    if (statusCode == -1)
                    {
                        statusCode = 400;
                        message = "No user with specified id found!";
                    }
                }
                else if (experienceDTO.Name == null && experienceDTO.Description == null)
                {
                    // no params given
                    if (statusCode == -1)
                    {
                        statusCode = 400;
                        message = "No fields specified!";
                    }
                }
                else
                {
                    // missing  
                    curExperience.Name = experienceDTO.Name == null ? curExperience.Name : experienceDTO.Name;
                    curExperience.Description = experienceDTO.Description == null ? curExperience.Description : experienceDTO.Description;


                    _context.Experiences.Update(curExperience);

                    if (_context.Hobbies.SaveChanges() > 0)
                    {
                        if (statusCode == -1)
                        {
                            statusCode = 200;
                            message = "Updated hobby '" + curExperience.Name + "' successfully!";
                        }
                    }
                }

            }
            catch
            {
                if (statusCode == -1)
                {
                    statusCode = 500;
                    message = "Failed to Update Experience: '" + experienceDTO.Name;
                }
            }
            finally
            {
                if (statusCode == -1)
                {
                    statusCode = 500;
                    message = "Invalid Id presented";
                }
            }
            return StatusCode(statusCode, message);
        }

        // delete
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteExperience(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not authorised. Please log in here: https://localhost:5001/Account/Login");
            }

            int statusCode = -1;
            string message = "";

            try
            {
                Experiences experience = _context.Experiences.GetById(id);

                if (experience == null)
                {
                    if (statusCode == -1)
                    {
                        statusCode = 500;
                        message = "No experience with specified id found!";
                    }
                }

                _context.Experiences.Delete(experience);

                if (_context.Experiences.SaveChanges() > 0)
                {
                    statusCode = 200;
                    message = "Deleted experience '" + experience.Name + "' successfully!";
                }
                else
                {
                    if (statusCode == -1)
                    {
                        statusCode = 500;
                        message = "Could not delete experience ";
                    }
                }
            }
            catch
            {
                if (statusCode == -1)
                {
                    statusCode = 500;
                    message = "Error deleting experience. Try again!";
                }
            }
            finally
            {
                if (statusCode == -1)
                {
                    statusCode = 500;
                    message = "Error deleting experience. Try again!";
                }
            }
            return StatusCode(statusCode, message);
        }
    }
}
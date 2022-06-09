using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Entities;
using API.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace API.Controllers
{
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
        public ActionResult<IEnumerable<Experiences>> GetExperiences()
        {
            return _context.Experiences.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Experiences> GetExperience(int id)
        {
            Experiences experience = _context.Experiences.GetById(id);
            if (experience == null)
            {
                return StatusCode(200, "Experience with Id " + id + " does not exist!");
            }
            else
            {
                return experience;
            }
        }

        // get all grads based on experience name
        [HttpGet("grads/{experienceName}")]
        public ActionResult<List<Grads>> GetGrads(string experienceName)
        {
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

                return grads;
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
        public ActionResult<Experiences> AddExperience(Experiences experience)
        {

            int statusCode = -1;
            string message = "";

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
                    experience.Id = 0;
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
        public ActionResult EditExperience(int id, Experiences experience)
        {

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
                        statusCode = 500;
                        message = "User with specified id not found!";
                    }
                }
                curExperience.Name = experience.Name;
                curExperience.Description = experience.Description;
                _context.Experiences.Update(curExperience);

                if (_context.Hobbies.SaveChanges() > 0)
                {
                    if (statusCode == -1)
                    {
                        statusCode = 200;
                        message = "Updated Hobby: '" + experience.Name + "' Successful!";
                    }
                }
            }
            catch
            {
                if (statusCode == -1)
                {
                    statusCode = 500;
                    message = "Failed to Update Experience: '" + experience.Name;
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
            Experiences experience = _context.Experiences.GetById(id);

            int statusCode = -1;
            string message = "";

            try
            {
                _context.Experiences.Delete(experience);

                if (_context.Experiences.SaveChanges() > 0)
                {
                    statusCode = 200;
                    message = "Deleted Experience: '" + experience.Name + "' Successfully!";
                }
                else
                {
                    if (statusCode == -1)
                    {
                        statusCode = 500;
                        message = "Failed to Delete Experience ";
                    }
                }
            }
            catch
            {
                if (statusCode == -1)
                {
                    statusCode = 500;
                    message = "Failed to Delete Experience ";
                }
            }
            finally
            {
                if (statusCode == -1)
                {
                    statusCode = 500;
                    message = "Failed to Delete Experience ";
                }
            }
            return StatusCode(statusCode, message);
        }
    }
}
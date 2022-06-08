using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Entities;
using API.Data;

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
        public ActionResult<IEnumerable<Experiences>> GetExperiences(){
            return _context.Experiences.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Experiences> GetExperience(int id){
           return _context.Experiences.GetById(id);
        }
        [HttpGet("getName/{id}")]
          public ActionResult<string> GetExperienceName(int id){
           return _context.Experiences.GetById(id).Name;
        }

    }
}
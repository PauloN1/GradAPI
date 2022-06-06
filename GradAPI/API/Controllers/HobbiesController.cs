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

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class HobbiesController : BaseAPIController
    {
        private readonly ILogger<HobbiesController> _logger;
        private readonly IRepositoryWrapper _context;

        public HobbiesController(ILogger<HobbiesController> logger, IRepositoryWrapper context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Hobbies>> GetHobbies(){
            return _context.Hobbies.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Hobbies> GetHobby(int id){
           return _context.Hobbies.GetById(id);
        }
    }
}
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

namespace API.Controllers {
    [Route("api/[controller]")] public class HobbiesController: BaseAPIController {
        private readonly ILogger < HobbiesController>_logger;
        private readonly IRepositoryWrapper _context;

        public HobbiesController(ILogger < HobbiesController > logger, IRepositoryWrapper context) {
            _logger=logger;
            _context=context;
        }

        [HttpGet]
         public ActionResult <IEnumerable<Hobbies>> GetHobbies() {
            return _context.Hobbies.GetAll().ToList();
        }

        [HttpGet("{id}")] 
        public ActionResult<Hobbies> GetHobby(int id) {
            return _context.Hobbies.GetById(id);
        }

        [HttpPost("add")] 
        public ActionResult<Hobbies> AddHobby(Hobbies Hobby) {
            _context.Hobbies.Create(Hobby);

            if (_context.Hobbies.SaveChanges() > 0) return CreatedAtAction(nameof(AddHobby), new {
                    id=Hobby.Id
                }

                , Hobby);

            return StatusCode(500, "Failed to Create Hobby: "+ Hobby.Name);
        }

        [HttpPut("edit/{id}")]
        public IActionResult UpdateHobby(int id, Hobbies Hobby) {
             Hobbies _hobby = _context.Hobbies.GetById(id);
            try{
                 
                  _hobby.Name = Hobby.Name;
                  _hobby.Description = Hobby.Description;
                  _context.Hobbies.Update(_hobby);

                 if (_context.Hobbies.SaveChanges() > 0) 
                 return StatusCode(200, "Updated Hobby: '"+ Hobby.Name+ "' Successful!");
              }
              catch{
                  return StatusCode(500, "Failed to Update Hobby: '"+ Hobby.Name + "'");
              }
           return NoContent();
        }
        
        [HttpDelete("delete/{id}")]
        public IActionResult DeleteHobby(int id) {
              Hobbies _hobby = _context.Hobbies.GetById(id);
            try{
                
                  _context.Hobbies.Delete(_hobby);

                 if (_context.Hobbies.SaveChanges() > 0) 
                 return StatusCode(200, "Delete Hobby: '"+ _hobby.Name+ "' Successful!");
              }
              catch{
                  return StatusCode(500, "Failed to Delete Hobby: '"+ _hobby.Name + "'");
              }
           return NoContent();
        }
    }
}
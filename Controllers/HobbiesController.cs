using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using API.Data;
using API.Entities;
using API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers {
    [Route("api/[controller]")] public class HobbiesController: BaseAPIController {
        private readonly ILogger<HobbiesController> _logger;
        private readonly IRepositoryWrapper _context;

        public HobbiesController(ILogger < HobbiesController > logger, IRepositoryWrapper context) {
            _logger=logger;
            _context=context;
        }

        [HttpGet]
         public ActionResult <IEnumerable<GradHobbiesDTO>> GetHobbies() {

            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not authorised. Please log in here: https://localhost:5001/Account/Login");
            }

            var hobbies = _context.Hobbies.GetAll().ToList();
            var _hobbies = new List<GradHobbiesDTO>();
            foreach (var item in hobbies)
            {
                _hobbies.Add(new GradHobbiesDTO{
                    Name = item.Name,
                    Description = item.Description
                });
            }
            return _hobbies;
        }

        [HttpGet("{id}")] 
        public ActionResult<GradHobbiesDTO> GetHobby(int id) {

            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not authorised. Please log in here: https://localhost:5001/Account/Login");
            }

            var hobby = _context.Hobbies.GetById(id);
            return new GradHobbiesDTO{
                Name = hobby.Name,
                Description = hobby.Description
            };
        }

        [HttpPost("add")] 
        public ActionResult<GradHobbiesDTO> AddHobby(GradHobbiesDTO Hobby) {

            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not authorised. Please log in here: https://localhost:5001/Account/Login");

            }
            var hobby = new Hobbies{
                Name = Hobby.Name,
                Description = Hobby.Description
            };
            _context.Hobbies.Create(hobby);

            if (_context.Hobbies.SaveChanges() > 0)
                  return CreatedAtAction(nameof(AddHobby), new
                  { 
                      id=hobby.Id
                  }, hobby);

            return StatusCode(500, "Failed to Create Hobby: "+ Hobby.Name);
        }

        [HttpPut("edit/{id}")]
        public IActionResult UpdateHobby(int id, GradHobbiesDTO Hobby) {

            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not authorised. Please log in here: https://localhost:5001/Account/Login");

            }
            if (!int.TryParse(id.ToString(), out id))
                return BadRequest("Invalid Id presented");
              
            try{
                 Hobbies _hobby = _context.Hobbies.GetById(id);
                if(_hobby == null){
                    return BadRequest("User with specified id not found!");
                }
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

            if (!User.Identity.IsAuthenticated)
            {
                return StatusCode(403, "You are not authorised. Please log in here: https://localhost:5001/Account/Login");

            }
            if (!int.TryParse(id.ToString(), out id))
                return BadRequest("Invalid Id presented");
              
            try{
                Hobbies _hobby = _context.Hobbies.GetById(id);
                if(_hobby == null){
                    return BadRequest("User with specified id not found!");
                }
                  _context.Hobbies.Delete(_hobby);

                 if (_context.Hobbies.SaveChanges() > 0) 
                 return StatusCode(200, "Delete Hobby: '"+ _hobby.Name+ "' Successful!");
              }
              catch{
                  return StatusCode(500, "Failed to Delete Hobby");
              }
           return NoContent();
        }
    }
}
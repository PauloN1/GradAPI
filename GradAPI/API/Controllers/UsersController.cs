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
    public class UsersController : BaseAPIController
    {
         private readonly IRepositoryWrapper _context;

        public UsersController(IRepositoryWrapper context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AppUser>> GetUsers(){
            return _context.AppUsers.GetAll().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<AppUser> GetUser(int id){
           return _context.AppUsers.GetById(id);
        }
    }
}
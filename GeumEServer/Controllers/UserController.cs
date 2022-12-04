using GeumEServer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeumEServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            User result = _context.Users
                .Where(item => item.UserId == id)
                .FirstOrDefault();

            return result;
        }

        [HttpGet]
        public List<User> GetUsers() 
        {
            List<User> results = _context.Users
                .OrderByDescending(item => item.UserId)
                .ToList();

            return results;
        }


    }
}

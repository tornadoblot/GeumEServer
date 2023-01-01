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

        // Create
        [HttpPost]
        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        // Read All User
        [HttpGet]
        public List<User> GetUsers()
        {
            List<User> results = _context.Users
                .OrderByDescending(item => item.UserId)
                .ToList();

            return results;
        }

        // Read
        [HttpGet("{email}")]
        public User GetUser(string email)
        {
            User result = _context.Users
                .Where(item => item.Email == email)
                .FirstOrDefault();

            return result;
        }

        // Update
        [HttpPut]
        public bool UpdateUser([FromBody] User user)
        {
            User findUser = _context.Users
                .Where(item => item.Email == user.Email)
                .FirstOrDefault();

            if (findUser == null)
                return false;

            findUser.Adress = user.Adress;
            findUser.Comment = user.Comment;

            _context.SaveChanges();

            return true;
        }

        // Update Password
        [HttpPut("password")]
        public bool UpdateUserPassword([FromBody]string[] info)
        {
            User findUser = _context.Users
                .Where(item => item.Email == info[0])
                .FirstOrDefault();

            if (findUser == null)
                return false;

            findUser.Password = info[1];

            _context.SaveChanges();

            return true;
        }

        // Delete
        [HttpDelete("{email}")]
        public bool DeleteUser(string email)
        {
            var findUser = _context.Users
                .Where(x => x.Email == email)
                .FirstOrDefault();

            if (findUser == null)
                return false;

            _context.Users.Remove(findUser);
            _context.SaveChanges();

            return true;
        }
    }
}

using GeumEServer.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeumEServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DogController : Controller
    {
        ApplicationDbContext _context;

        public DogController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public Dog CreateDog([FromBody] string[] info)
        {
            User owner = _context.Users
                .Where(item => item.Email == info[0])
                .FirstOrDefault();

            if (owner == null)
                return null;

            Dog dog = new Dog
            {
                UserId = owner.UserId,
                UserEmail = owner.Email,
                Name = info[1],
                Species = info[2],
                Birth = Convert.ToDateTime(info[3])
            };

            _context.Dogs.Add(dog);
            _context.SaveChanges();

            return dog;
        }

        [HttpGet]
        public List<Dog> GetDogs()
        {
            List<Dog> results = _context.Dogs
                .OrderByDescending(item => item.Id)
                .ToList();

            return results;
        }

        [HttpGet("{email}")]
        public Dog GetDog(string email)
        {
            Dog results = _context.Dogs
                .Where(item => item.UserEmail == email)
                .FirstOrDefault();

            return results;
        }

        [HttpPut]
        public bool UpdateDog([FromBody] Dog dog)
        {
            Dog findDog = _context.Dogs
                .Where(item => item.UserEmail == dog.UserEmail)
                .FirstOrDefault();

            if (findDog == null)
                return false;

            findDog.Name = dog.Name;

            _context.SaveChanges();

            return true;
        }

        [HttpDelete("{email}")]
        public bool DeleteDog(string email)
        {
            var findDog = _context.Dogs
                .Where(x => x.UserEmail == email)
                .FirstOrDefault();

            if (findDog == null)
                return false;

            _context.Dogs.Remove(findDog);
            _context.SaveChanges();

            return true;
        }
    }
}
